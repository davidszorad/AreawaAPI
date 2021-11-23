using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Areawa;

public interface IApiKeyValidator
{
    Task<(bool isValid, Guid userPublicId)> ValidateAsync(HttpRequest request, CancellationToken cancellationToken = default);
}