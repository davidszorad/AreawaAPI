using System.Net;
using Configuration;
using Core.WebsiteArchiveCreator;
using Domain.Enums;

namespace Awa;

internal class HttpService
{
    private static HttpClient _httpClient = new();

    public async Task PostAsync(string apiKey, Stream paramFileStream, CancellationToken cancellationToken = default)
    {
        var command = new CreateArchivedWebsiteCommand
        {
            Name = "subor",
            ArchiveType = ArchiveType.Pdf,
            Description = "docasnyfolder",
            SourceUrl = "https://dev-trips.com/dev/how-to-create-classes-that-protect-its-data"
        };
        
        HttpContent fileStreamContent = new StreamContent(paramFileStream);
        var formData = new MultipartFormDataContent();
        formData.Add(fileStreamContent, "file", "file");
        
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
        response.EnsureSuccessStatusCode();
    }

    private static string GetUrl(CreateArchivedWebsiteCommand command) =>
        $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiScreenshotUrl}" +
        $"?{nameof(CreateArchivedWebsiteCommand.Name)}={command.Name}" +
        $"&{nameof(CreateArchivedWebsiteCommand.Description)}={command.Description}" +
        $"&{nameof(CreateArchivedWebsiteCommand.SourceUrl)}={command.SourceUrl}" +
        $"&{nameof(CreateArchivedWebsiteCommand.ArchiveType)}={command.ArchiveType}";
}