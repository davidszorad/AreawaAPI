using System.Threading;
using System.Threading.Tasks;
using Configuration;
using Core.Shared;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure;

public class EmailService : IEmailService
{
    public async Task SendAsync(EmailContent emailContent, CancellationToken cancellationToken = default)
    {
        var apiKey = ConfigStore.GetValue(ConfigurationConstants.SendGridApiKey);
        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage
        {
            From = new EmailAddress(ConfigurationConstants.SenderEmail, ConfigurationConstants.SenderName),
            Subject = emailContent.Subject,
            PlainTextContent = emailContent.Body
        };
        msg.AddTo(new EmailAddress(emailContent.RecipientEmail, emailContent.RecipientName));
        var response = await client.SendEmailAsync(msg, cancellationToken);
        
        //Console.WriteLine(response.IsSuccessStatusCode ? "Email queued successfully!" : "Something went wrong!");
    }
}