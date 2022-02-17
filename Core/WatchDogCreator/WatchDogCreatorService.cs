using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Database;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Core.WatchDogCreator;

public class WatchDogCreatorService : IWatchDogCreatorService
{
    private readonly IHttpService _httpService;
    private readonly IQueueService _queueService;
    private readonly AreawaDbContext _dbContext;
    private readonly IEmailService _emailService;

    public WatchDogCreatorService(
        IHttpService httpService,
        IQueueService queueService,
        AreawaDbContext dbContext,
        IEmailService emailService)
    {
        _httpService = httpService;
        _queueService = queueService;
        _dbContext = dbContext;
        _emailService = emailService;
    }
    
    public async Task<string> GetSourcePreviewAsync(SourcePreviewCommand command, CancellationToken cancellationToken = default)
    {
        var htmlSource = await _httpService.GetHtmlSourceAsync(command.Url, cancellationToken);

        return command.HtmlContentOption switch
        {
            HtmlContentOption.Full => htmlSource,
            HtmlContentOption.StripStylesAndScripts => htmlSource.StripScripts().StripStyles(),
            HtmlContentOption.StripAll => htmlSource.StripHead().StripScripts().StripStyles(),
            _ => htmlSource
        };
    }

    public async Task<string> PreviewAsync(CreateWatchDogCommand command, CancellationToken cancellationToken = default)
    {
        if (!await _httpService.IsStatusOkAsync(command.Url, cancellationToken))
        {
            throw new Exception();
        }

        var watchedHtml = await TryGetWatchedHtmlAsync(command.Url, command.StartSelector, command.EndSelector, cancellationToken);
        if (!watchedHtml.isSuccess)
        {
            throw new Exception();
        }

        return watchedHtml.content;
    }

    public async Task<Guid> ScheduleAsync(CreateWatchDogCommand command, Guid userPublicId, CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.ApiUser.SingleAsync(x => x.PublicId == userPublicId, cancellationToken);
        
        var watchDog = command.Map();
        watchDog.PublicId = Guid.NewGuid();
        watchDog.ApiUser = user;
        watchDog.EntityStatusId = Status.Pending;
        watchDog.IsActive = true;

        _dbContext.WatchDog.Add(watchDog);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        await _queueService.InsertMessageAsync(ConfigurationConstants.WatchDogIncomingQueue, watchDog.PublicId.ToString(), cancellationToken);

        return watchDog.PublicId;
    }

    public async Task ProcessNewAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        var watchDog = await _dbContext.WatchDog.SingleAsync(x => x.PublicId == publicId, cancellationToken);
        
        watchDog.EntityStatusId = Status.Processing;
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (!await _httpService.IsStatusOkAsync(watchDog.Url, cancellationToken))
        {
            watchDog.EntityStatusId = Status.SourceNotFound;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        var watchedHtml = await TryGetWatchedHtmlAsync(watchDog.Url, watchDog.StartSelector, watchDog.EndSelector, cancellationToken);
        if (!watchedHtml.isSuccess)
        {
            watchDog.EntityStatusId = Status.Error;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
        
        var contentHash = Sha256ToString(watchedHtml.content);
        watchDog.ContentHash = contentHash;
        watchDog.EntityStatusId = Status.Ok;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task CheckChangesAsync(CancellationToken cancellationToken = default)
    {
        var watchDogs = await (
                from wd in _dbContext.WatchDog
                join u in _dbContext.ApiUser on wd.ApiUserId equals u.ApiUserId
                where wd.IsActive && wd.EntityStatusId == Status.Ok && u.IsActive && u.IsPremium
                select wd)
            .Include(x => x.ApiUser)
            .ToListAsync(cancellationToken);
        
        foreach (var watchDog in watchDogs)
        {
            watchDog.ScanCount += 1;
            
            if (!IsWatchDogActive(watchDog))
            {
                watchDog.IsActive = false;
                await _dbContext.SaveChangesAsync(cancellationToken);

                var emailMessageTemplate = new EmailMessageTemplate(EmailMessageTemplate.TemplateType.CheckPeriodEnded);
                await _emailService.SendAsync(emailMessageTemplate.GetEmailContent(watchDog), cancellationToken);
                
                continue;
            }
            
            if (!await _httpService.IsStatusOkAsync(watchDog.Url, cancellationToken))
            {
                watchDog.EntityStatusId = Status.SourceNotFound;
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                var emailMessageTemplate = new EmailMessageTemplate(EmailMessageTemplate.TemplateType.SourceNotFound);
                await _emailService.SendAsync(emailMessageTemplate.GetEmailContent(watchDog), cancellationToken);
                
                continue;
            }

            var watchedHtml = await TryGetWatchedHtmlAsync(watchDog.Url, watchDog.StartSelector, watchDog.EndSelector, cancellationToken);
            if (!watchedHtml.isSuccess)
            {
                watchDog.EntityStatusId = Status.Error;
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                var emailMessageTemplate = new EmailMessageTemplate(EmailMessageTemplate.TemplateType.ErrorWhileParsingHtml);
                await _emailService.SendAsync(emailMessageTemplate.GetEmailContent(watchDog), cancellationToken);
                
                continue;
            }
        
            var contentHash = Sha256ToString(watchedHtml.content);
            if (!string.Equals(watchDog.ContentHash, contentHash, StringComparison.Ordinal))
            {
                watchDog.EntityStatusId = Status.SourceChanged;
                await _dbContext.SaveChangesAsync(cancellationToken);
                
                var emailMessageTemplate = new EmailMessageTemplate(EmailMessageTemplate.TemplateType.SourceChanged);
                await _emailService.SendAsync(emailMessageTemplate.GetEmailContent(watchDog), cancellationToken);
            }
            else
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }

    public async Task DeleteAsync(Guid publicId, Guid userPublicId, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.WatchDog
            .Include(x => x.ApiUser)
            .SingleAsync(x => x.PublicId.Equals(publicId) && x.ApiUser.PublicId == userPublicId, cancellationToken);
        _dbContext.WatchDog.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    private async Task<(bool isSuccess, string content)> TryGetWatchedHtmlAsync(string url, string startSelector, string endSelector, CancellationToken cancellationToken = default)
    {
        var httpContent = await _httpService.GetHtmlSourceAsync(url, cancellationToken);

        string watchedHtmlContent;
        try
        {
            if (string.IsNullOrWhiteSpace(endSelector))
            {
                watchedHtmlContent = startSelector;
                if (!httpContent.Contains(watchedHtmlContent))
                {
                    throw new Exception();
                }
            }
            else
            {
                try
                {
                    watchedHtmlContent = httpContent
                        .Split(startSelector)[1]
                        .Split(endSelector)[0];

                    if (watchedHtmlContent.Length == 0)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }
        catch (Exception)
        {
            return (false, null);
        }
        
        return (true, watchedHtmlContent);
    }
    
    private static string Sha256ToString(string s)
    {
        using (var alg = SHA256.Create())
        {
            return alg
                .ComputeHash(Encoding.UTF8.GetBytes(s))
                .Aggregate(new StringBuilder(), (sb, x) => sb.Append(x.ToString("x2")))
                .ToString();
        }
    }
    
    private bool IsWatchDogActive(Database.Entities.WatchDog watchDog)
    {
        TimeSpan ts = DateTime.UtcNow - watchDog.CreatedOn;
        
        switch (watchDog.RetryPeriodId)
        {
            case RetryPeriod.OneWeek:
                return ts.Days <= 7;
            case RetryPeriod.OneMonth:
                return ts.Days <= 30;
            case RetryPeriod.TreeMonths:
                return ts.Days <= 90;
            case RetryPeriod.OneYear:
                return ts.Days <= 365;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}