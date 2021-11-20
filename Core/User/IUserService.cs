using System.Threading;
using System.Threading.Tasks;

namespace Core.User;

public interface IUserService
{
    Task CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
}