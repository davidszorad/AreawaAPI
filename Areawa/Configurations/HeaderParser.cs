using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Areawa;

internal static class HeaderParser
{
    public static bool TryGetGetApiKey(HttpRequest request, out Guid apiUserPublicId)
    {
        apiUserPublicId = Guid.Empty;
        var apiKey = request.Headers.FirstOrDefault(x => x.Key.Equals("X-ApiKey"));
        if ((apiKey.Key, apiKey.Value) != default && Guid.TryParse(apiKey.Value, out apiUserPublicId))
        {
            return true;
        }
        return false;
    }
}