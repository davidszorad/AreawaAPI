using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.UserManagement;

public interface IUserService
{
    Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
    Task<bool> IsActiveAsync(Guid publicId, CancellationToken cancellationToken = default);
    Task<bool> IsPremiumAsync(Guid publicId, CancellationToken cancellationToken = default);
}