using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared;

public interface IStorageService
{
    Task<string> CreateAsync(string folder, string file, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string folder, string fileName, CancellationToken cancellationToken = default);
    Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default);
}