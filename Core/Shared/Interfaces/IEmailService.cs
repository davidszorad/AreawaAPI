using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IEmailService
{
    Task SendAsync(EmailContent emailContent, CancellationToken cancellationToken = default);
}