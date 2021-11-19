using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IScreenshotCreator
{
    Task<Stream> TakeScreenshotStreamAsync(ArchiveFile file, CancellationToken cancellationToken = default);
}