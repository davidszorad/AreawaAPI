using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Core.Shared;

public interface IStorageService
{
    Task<string> UploadAsync(Stream stream, ArchiveType archiveType, string folder, string file, CancellationToken cancellationToken = default);
    Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default);
}