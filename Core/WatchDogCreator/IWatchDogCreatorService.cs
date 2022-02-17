using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.WatchDogCreator;

public interface IWatchDogCreatorService
{
    Task<string> GetSourcePreviewAsync(SourcePreviewCommand command, CancellationToken cancellationToken = default);
    Task<string> PreviewAsync(CreateWatchDogCommand command, CancellationToken cancellationToken = default);
    Task<Guid> ScheduleAsync(CreateWatchDogCommand command, Guid userPublicId, CancellationToken cancellationToken = default);
    Task ProcessNewAsync(Guid publicId, CancellationToken cancellationToken = default);
    Task CheckChangesAsync(CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid publicId, Guid userPublicId, CancellationToken cancellationToken = default);
}