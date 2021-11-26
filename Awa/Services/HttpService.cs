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
        
        var httpRequestMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(GetUrl(archiveFile)),
            Headers = { 
                { HttpRequestHeader.Accept.ToString(), "application/json" },
                { "X-ApiKey", apiKey }
            },
            Content = formData
        };
        
        var response = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    private static string GetUrl(ArchiveFile archiveFile)
        => $"{ConfigurationConstants.ApiRootUrl}/{ConfigurationConstants.ApiScreenshotUrl}" +
           $"?{nameof(ArchiveFile.Filename)}={archiveFile.Filename}" +
           $"&{nameof(ArchiveFile.Extension)}={archiveFile.Extension}" +
           $"&{nameof(ArchiveFile.Folder)}={archiveFile.Folder}" +
           $"&{nameof(ArchiveFile.SourceUrl)}={archiveFile.SourceUrl}";
}