using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared
{
    internal class HttpService
    {
        public async Task<bool> IsStatusOkAsync(string url, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            return true;
        }
    }
}