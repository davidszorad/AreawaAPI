using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;

namespace Infrastructure;

public class HttpService : IHttpService
{
    public async Task<string> GetHtmlSourceAsync(string url, CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await HttpClientFactory.GetInstance().GetAsync(url, cancellationToken);
        using HttpContent content = response.Content;
        return await content.ReadAsStringAsync(cancellationToken);
    }
    
    public async Task<bool> IsStatusOkAsync(string url, CancellationToken cancellationToken = default)
    {
        try
        {
            var checkingResponse = await HttpClientFactory.GetInstance().GetAsync(url, cancellationToken);
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