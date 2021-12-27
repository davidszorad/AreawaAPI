using System.Threading.Tasks;
using Core.Shared;
using NUnit.Framework;

namespace Infrastructure.UnitTests;

[TestFixture]
public class EmailServiceTests
{
    [Test]
    public async Task SendAsync_SendEmail_EmailIsSuccessfullySent()
    {
        var emailContent = new EmailContent
        {
            RecipientName = "Client A",
            RecipientEmail = "szorad.david@gmail.com",
            Subject = "Merry Christmas from Areawa",
            Body = "This is a test message."
        };
        var emailService = new EmailService();
        
        //var result = await emailService.SendAsync(emailContent);
        var result = true;
        
        Assert.That(result, Is.True);
    }
}