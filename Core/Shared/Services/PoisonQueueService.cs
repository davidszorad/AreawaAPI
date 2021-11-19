using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public class PoisonQueueService : IPoisonQueueService
{
    private readonly IQueueService _queueService;

    public PoisonQueueService(IQueueService queueService)
    {
        _queueService = queueService;
    }

    public async Task InsertMessageAsync(string originQueueName, string message, Exception exception, CancellationToken cancellationToken = default)
    {
        await _queueService.InsertMessageAsync(
            $"poison-{originQueueName}",
            JsonSerializer.Serialize(new { Message = message, Exception = exception }),
            cancellationToken);
    }
}