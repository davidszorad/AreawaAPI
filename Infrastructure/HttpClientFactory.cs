using System.Net.Http;

namespace Infrastructure;

public static class HttpClientFactory
{
    private static HttpClient _httpClient = new();

    public static HttpClient GetInstance()
    {
        return _httpClient;
    }
}