using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.UserManagement;

public class UserService : IUserService
{
    private readonly AreawaDbContext _areawaDbContext;

    public UserService(AreawaDbContext areawaDbContext)
    {
        _areawaDbContext = areawaDbContext;
    }
    
    public async Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = new ApiUser
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            PublicId = Guid.NewGuid(),
            IsActive = false,
            IsPremium = false
        };

        _areawaDbContext.ApiUser.Add(user);
        await _areawaDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsActiveAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        var user = await _areawaDbContext.ApiUser.FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
        return user is { IsActive: true };
    }

    public async Task<bool> IsPremiumAsync(Guid publicId, CancellationToken cancellationToken = default)
    {
        var user = await _areawaDbContext.ApiUser.FirstOrDefaultAsync(x => x.PublicId == publicId, cancellationToken);
        return user is { IsActive: true, IsPremium: true };
    }
}