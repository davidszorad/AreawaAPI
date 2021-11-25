using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;

namespace Core.Shared;

public interface IScreenshotCreator
{
    Task<Stream> TakeScreenshotStreamAsync(ArchiveFile file, CancellationToken cancellationToken = default);
}

public interface IShit
{
    string Get();
}