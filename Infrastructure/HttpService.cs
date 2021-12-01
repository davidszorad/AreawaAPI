using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure;

public class HttpService
{
    public async Task<string> GetHtmlBodyAsync(string url, CancellationToken cancellationToken = default)
    {
        using (HttpResponseMessage response = await HttpClientFactory.GetInstance().GetAsync(url, cancellationToken))
        {
            using (HttpContent content = response.Content)
            {
                var htmlContent = await content.ReadAsStringAsync(cancellationToken);

                return htmlContent
                    .StripHead()
                    .StripScripts()
                    .StripStyles();
            }
        }
    }
}