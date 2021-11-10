using System.Threading;
using System.Threading.Tasks;

namespace Core.Shared
{
    public interface IScreenshotCreator
    {
        Task<string> CreateAsync(string sourceUrl, CancellationToken cancellationToken = default);
    }
}