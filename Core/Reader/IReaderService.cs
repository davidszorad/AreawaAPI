using Core.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Reader
{
    public interface IReaderService
    {
        Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default);
    }
}
