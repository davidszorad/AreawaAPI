using System.Threading;
using System.Threading.Tasks;

namespace Core.WatchDogReader;

public interface IWatchDogReaderService
{
    Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default);
}