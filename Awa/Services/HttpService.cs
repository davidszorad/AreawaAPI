using System.Net;
using Configuration;
using Core.WebsiteArchiveCreator;

namespace Awa;

internal class HttpService
{
    private static HttpClient _httpClient = new();

    public async Task PostAsync(string apiKey, Stream paramFileStream, CreateArchivedWebsiteCommand command, CancellationToken cancellationToken = default)
    {
        HttpContent fileStreamContent = new StreamContent(paramFileStream);
        var formData = new MultipartFormDataContent();
        formData.Add(fileStreamContent, "file", "filename");
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(GetUrl(command)),
            Headers = { 
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-ApiKey", apiKey }
            },
            Content = formData
        };
        
        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        
        // TODO: process response
        
        response.EnsureSuccessStatusCode();
    }

    private static string GetUrl(CreateArchivedWebsiteCommand command) =>
        $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiUrlWebsiteArchiveCreate}" +
        $"?{nameof(CreateArchivedWebsiteCommand.Name)}={command.Name}" +
        $"&{nameof(CreateArchivedWebsiteCommand.Description)}={command.Description}" +
        $"&{nameof(CreateArchivedWebsiteCommand.SourceUrl)}={command.SourceUrl}" +
        $"&{nameof(CreateArchivedWebsiteCommand.ArchiveType)}={command.ArchiveType}";
}