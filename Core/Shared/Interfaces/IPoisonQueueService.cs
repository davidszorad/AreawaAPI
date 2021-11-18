using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IPoisonQueueService
{
    Task InsertMessageAsync(string originQueueName, string message, Exception exception, CancellationToken cancellationToken = default);
}