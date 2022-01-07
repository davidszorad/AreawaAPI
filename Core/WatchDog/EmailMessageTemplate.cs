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

    private readonly TemplateType _templateType;
    
    private static string CheckPeriodEndedSubject => "Areawa - Retry period ended";
    private static string SourceNotFoundSubject => "Areawa - Source not found";
    private static string ErrorWhileParsingHtmlSubject => "Areawa - Error while parsing HTML";
    private static string SourceChangedSubject => "Areawa - Source changed";

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
            Subject = GetSubject(watchDog.Name),
            Body = GetBody(watchDog)
        };
    }
    
    private string GetSubject(string title)
    {
        switch (_templateType)
        {
            case TemplateType.CheckPeriodEnded:
                return $"{CheckPeriodEndedSubject} - {title}";
            case TemplateType.SourceNotFound:
                return $"{SourceNotFoundSubject} - {title}";
            case TemplateType.ErrorWhileParsingHtml:
                return $"{ErrorWhileParsingHtmlSubject} - {title}";
            case TemplateType.SourceChanged:
                return $"{SourceChangedSubject} - {title}";
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private string GetBody(Database.Entities.WatchDog watchDog)
    {
        var sb = new StringBuilder();
        
        switch (_templateType)
        {
            case TemplateType.CheckPeriodEnded:
                sb.AppendLine("Areawa will no longer check for changes because the retry period ended.");
                break;
            case TemplateType.SourceNotFound:
                sb.AppendLine("The source was not found.");
                break;
            case TemplateType.ErrorWhileParsingHtml:
                sb.AppendLine("Error while parsing HTML.");
                break;
            case TemplateType.SourceChanged:
                sb.AppendLine("Areawa watchdog has found a change.");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        AppendDetails(sb, watchDog);
        return sb.ToString();
    }

    private void AppendDetails(StringBuilder sb, Database.Entities.WatchDog watchDog)
    {
        sb.AppendLine(string.Empty);
        sb.AppendLine("Details:");
        sb.AppendLine($"Name: {watchDog.Name}");
        sb.AppendLine($"URL: {watchDog.Url}");
        sb.AppendLine($"ID: {watchDog.PublicId}");
        sb.AppendLine($"Retry period: {GetRetryPeriodDays(watchDog.RetryPeriodId)}");
        sb.AppendLine($"Created on: {watchDog.CreatedOn.ToString("yyyy MMMM dd")}");
        sb.AppendLine($"Scan count: {watchDog.ScanCount}x");
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