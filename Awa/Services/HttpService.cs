using System.Net;
using System.Text.Json;
using Configuration;
using Domain.Enums;
using Domain.Models;

namespace Awa;

internal class HttpService
{
    private static HttpClient _httpClient = new();
    
    public HttpService()
    {
        
    }

    public async Task PostAsync(string apiKey, Stream paramFileStream, CancellationToken cancellationToken = default)
    {
        var archiveFile = new ArchiveFile
        {
            Filename = "subor",
            Extension = ArchiveType.Pdf,
            Folder = "docasnyfolder",
            SourceUrl = "https://dev-trips.com/dev/how-to-create-classes-that-protect-its-data"
        };
        
        HttpContent fileStreamContent = new StreamContent(paramFileStream);
        var formData = new MultipartFormDataContent();
        formData.Add(fileStreamContent, "file", "file");
        formData.Add(new StringContent(JsonSerializer.Serialize(archiveFile)), "archiveFile");
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiScreenshotUrl}"),
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