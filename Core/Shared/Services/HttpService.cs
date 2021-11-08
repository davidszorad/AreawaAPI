using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("Core.UnitTests")]

namespace Core.Shared
{
    internal class HttpService
    {
        private static HttpClient Client = new();

        public async Task<bool> IsStatusOkAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkingResponse = await Client.GetAsync(url, cancellationToken);
                return checkingResponse.IsSuccessStatusCode && checkingResponse.RequestMessage.RequestUri.Equals(url);
            }
            catch (HttpRequestException ex)
            {
                if (ex.Message.Contains("No such host is known", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}