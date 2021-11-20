using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Areawa;

public class ApiKeyHeaderSwaggerAttribute : IOperationFilter
{

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "X-ApiKey",
            In = ParameterLocation.Header,
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String"
            }
        });
    }
}

public static class HeaderParser
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