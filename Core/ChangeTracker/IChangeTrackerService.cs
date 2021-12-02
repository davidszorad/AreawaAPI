using System.Threading;
using System.Threading.Tasks;

namespace Core.ChangeTracker;

public interface IChangeTrackerService
{
    Task<string> GetSourcePreviewAsync(SourcePreviewCommand command, CancellationToken cancellationToken = default);
}