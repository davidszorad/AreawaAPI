using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IScreenshotCreator
{
    Task<string> TakeScreenshotAsync(ArchiveFile file, CancellationToken cancellationToken = default);
    Task<Stream> TakeScreenshotStreamAsync(ArchiveFile file, CancellationToken cancellationToken = default);
    void Cleanup(string screenshotPath);
}