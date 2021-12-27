using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IEmailService
{
    Task<bool> SendAsync(EmailContent emailContent, CancellationToken cancellationToken = default);
}