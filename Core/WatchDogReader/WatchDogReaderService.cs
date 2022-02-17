using System.Threading;
using System.Threading.Tasks;

namespace Core.WatchDogReader;

public class WatchDogReaderService : IWatchDogReaderService
{
    public Task<QueryResult<WatchDogQueryResult>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}