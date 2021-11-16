using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IScreenshotCreator
{
    Task<string> TakeScreenshotAsync(ArchiveFile file, CancellationToken cancellationToken = default);
    void Cleanup(string screenshotPath);
}