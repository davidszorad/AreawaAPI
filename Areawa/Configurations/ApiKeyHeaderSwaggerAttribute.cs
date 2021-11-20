using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Areawa;

internal class ApiKeyHeaderSwaggerAttribute : IOperationFilter
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