using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IQueueService
{
    Task InsertMessageAsync(string queueName, string message, CancellationToken cancellationToken = default);
}