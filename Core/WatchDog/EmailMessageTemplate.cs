using System;
using System.Text;
using Core.Shared;
using Domain.Enums;

namespace Core.WatchDog;

internal class EmailMessageTemplate
{
    public enum TemplateType
    {
        CheckPeriodEnded = 1,
        SourceNotFound = 2,
        ErrorWhileParsingHtml = 3,
        SourceChanged = 4
    }

    private TemplateType _templateType;
    
    private static string CheckPeriodEndedSubject => "Areawa - Retry period ended";
    private static string SourceNotFoundSubject => "Areawa - Source not found";
    private static string ErrorWhileParsingHtmlSubject => "Areawa - Source not found";
    private static string SourceChangedSubject => "Areawa - Source not found";

    public EmailMessageTemplate(TemplateType templateType)
    {
        _templateType = templateType;
    }

    public EmailContent GetEmailContent(Database.Entities.WatchDog watchDog)
    {
        return new EmailContent
        {
            RecipientEmail = watchDog.ApiUser.Email,
            RecipientName = $"{watchDog.ApiUser.FirstName} {watchDog.ApiUser.LastName}",
            Subject = GetSubject(),
            Body = GetBody(watchDog)
        };
    }
    
    public string GetSubject()
    {
        switch (_templateType)
        {
            case TemplateType.CheckPeriodEnded:
                return CheckPeriodEndedSubject;
            case TemplateType.SourceNotFound:
                return SourceNotFoundSubject;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    public string GetBody(Database.Entities.WatchDog watchDog)
    {
        switch (_templateType)
        {
            case TemplateType.CheckPeriodEnded:
                return CheckPeriodEnded(watchDog);
            case TemplateType.SourceNotFound:
                return SourceNotFound(watchDog);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private string CheckPeriodEnded(Database.Entities.WatchDog watchDog)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Areawa will no longer check for changes because the retry period ended.");
        sb.AppendLine(string.Empty);
        sb.AppendLine("Details:");
        sb.AppendLine($"Name: {watchDog.Name}");
        sb.AppendLine($"URL: {watchDog.Url}");
        sb.AppendLine($"ID: {watchDog.PublicId}");
        sb.AppendLine($"Retry period: {GetRetryPeriodDays(watchDog.RetryPeriodId)}");
        sb.AppendLine($"Created on: {watchDog.CreatedOn.ToString("yyyy MMMM dd")}");
        return sb.ToString();
    }

    private string SourceNotFound(Database.Entities.WatchDog watchDog)
    {
        return "";
    }

    private int GetRetryPeriodDays(RetryPeriod retryPeriod)
    {
        switch (retryPeriod)
        {
            case RetryPeriod.OneWeek:
                return 7;
            case RetryPeriod.OneMonth:
                return 30;
            case RetryPeriod.TreeMonths:
                return 90;
            case RetryPeriod.OneYear:
                return 365;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}