using System;
using System.Threading;
using System.Threading.Tasks;
using Core.UserManagement;
using Microsoft.AspNetCore.Http;

namespace Areawa;

internal class ApiKeyValidator : IApiKeyValidator
{
    private readonly IUserService _userService;

    public ApiKeyValidator(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<(bool isValid, Guid userPublicId)> ValidateAsync(HttpRequest request, CancellationToken cancellationToken = default)
    {
        if (!HeaderParser.TryGetGetApiKey(request, out var userPublicId))
        {
            return (isValid: false, userPublicId: userPublicId);
        }

        var isActive = await _userService.IsActiveAsync(userPublicId, cancellationToken);
        
        return (isValid: isActive, userPublicId: userPublicId);
    }
}