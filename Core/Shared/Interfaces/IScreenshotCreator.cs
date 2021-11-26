using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Core.Shared;

public interface IScreenshotCreator
{
    Task<Stream> TakeScreenshotStreamAsync(string sourceUrl, ArchiveType archiveType, CancellationToken cancellationToken = default);
}