using System.Net;
using Configuration;
using Core.WebsiteArchiveCreator;
using Infrastructure;

namespace Awa;

internal class HttpService
{
    public async Task<string> PostAsync(string apiKey, Stream paramFileStream, CreateArchivedWebsiteCommand command, CancellationToken cancellationToken = default)
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
        
        var response = await HttpClientFactory.GetInstance().SendAsync(httpRequestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync(cancellationToken);
    }

    private static string GetUrl(CreateArchivedWebsiteCommand command) =>
        $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiUrlWebsiteArchiveCreate}" +
        $"?{nameof(CreateArchivedWebsiteCommand.Name)}={command.Name}" +
        $"&{nameof(CreateArchivedWebsiteCommand.Description)}={command.Description}" +
        $"&{nameof(CreateArchivedWebsiteCommand.SourceUrl)}={command.SourceUrl}" +
        $"&{nameof(CreateArchivedWebsiteCommand.ArchiveType)}={command.ArchiveType}";
}