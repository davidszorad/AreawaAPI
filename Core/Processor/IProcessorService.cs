using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Core.Processor
{
    public interface IProcessorService
    {
        Task<(bool isSuccess, Status status)> ProcessAsync(Guid publicId, CancellationToken cancellationToken = default);
    }
}
