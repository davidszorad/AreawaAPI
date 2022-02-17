using System.Threading;
using System.Threading.Tasks;

namespace Core.WatchDogReader;

public interface IWatchDogReaderService
{
    Task<QueryResult<WatchDogQueryResult>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default);
}