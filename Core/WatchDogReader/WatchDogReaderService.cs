using System.Threading;
using System.Threading.Tasks;

namespace Core.WatchDogReader;

public class WatchDogReaderService : IWatchDogReaderService
{
    public Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}