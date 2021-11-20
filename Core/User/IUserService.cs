using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.User;

public interface IUserService
{
    Task<Guid> CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
}