using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IHttpService
{
    Task<string> GetHtmlSourceAsync(string url, CancellationToken cancellationToken = default);
    Task<bool> IsStatusOkAsync(string url, CancellationToken cancellationToken = default);
}