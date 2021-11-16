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
        private static readonly HttpClient Client = new();

        public async Task<bool> IsStatusOkAsync(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkingResponse = await Client.GetAsync(url, cancellationToken);
                return checkingResponse.IsSuccessStatusCode &&
                       checkingResponse.RequestMessage?.RequestUri != null &&
                       checkingResponse.RequestMessage.RequestUri.Equals(new Uri(url));
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}