using System.Threading;
using System.Threading.Tasks;
using Core.Shared;

namespace Core.WebsiteArchiveReader
{
    public interface IWebsiteArchiveReaderService
    {
        Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default);
    }
}
