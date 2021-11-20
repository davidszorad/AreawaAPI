using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Core.Database.Entities;

namespace Core.User;

public class UserService : IUserService
{
    private readonly AreawaDbContext _areawaDbContext;

    public UserService(AreawaDbContext areawaDbContext)
    {
        _areawaDbContext = areawaDbContext;
    }
    
    public async Task<Guid> CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
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

        return user.PublicId;
    }
}