using System.Net;
using System.Text;
using Configuration;
using Core.WebsiteArchiveCreator;
using Infrastructure;

namespace Awa;

internal class HttpService
{
    public async Task<string> PostAsync(string apiKey, string shortId, Stream paramFileStream, CancellationToken cancellationToken = default)
    {
        HttpContent fileStreamContent = new StreamContent(paramFileStream);
        var formData = new MultipartFormDataContent();
        formData.Add(fileStreamContent, "file", "filename");
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(GetUploadUrl(shortId)),
            Headers = { 
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-ApiKey", apiKey }
            },
            Content = formData
        };
        
        var response = await HttpClientFactory.GetInstance().SendAsync(httpRequestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
    
    public async Task<string> PostAsync(string apiKey, CreateArchivedWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        if (command == null)
        {
            throw new ArgumentNullException(nameof(CreateArchivedWebsiteCommand));
        }
        
        var content = new StringContent(command.ToString() ?? string.Empty, Encoding.UTF8, "application/json");
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(GetCreateUrl()),
            Headers = { 
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-ApiKey", apiKey }
            },
            Content = content
        };
        
        var response = await HttpClientFactory.GetInstance().SendAsync(httpRequestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private static string GetCreateUrl() =>
        $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiUrlWebsiteArchiveCreate}";
    
    private static string GetUploadUrl(string shortId) =>
        $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiUrlWebsiteArchiveUpload}?ShortId={shortId}";
}