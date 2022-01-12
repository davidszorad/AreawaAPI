using System.Net.Http;

namespace Infrastructure;

public class HttpClientFactory
{
    private static HttpClient _httpClient = new();

    public static HttpClient GetInstance()
    {
        return _httpClient;
    }
}