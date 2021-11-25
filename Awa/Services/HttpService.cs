using System.Net;
using Configuration;

namespace Awa;

internal class HttpService
{
    private static HttpClient _httpClient = new();
    
    public HttpService()
    {
        
    }

    public async Task PostAsync(string apiKey, Stream paramFileStream, CancellationToken cancellationToken = default)
    {
        HttpContent fileStreamContent = new StreamContent(paramFileStream);
        var formData = new MultipartFormDataContent();
        formData.Add(fileStreamContent, "file", "file");
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Path.Combine(ConfigurationConstants.ApiRootUrl, ConfigurationConstants.ApiScreenshotUrl)),
            Headers = { 
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-ApiKey", apiKey }
            },
            Content = formData
        };
        
        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}