using Core.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Reader
{
    public interface IReaderService
    {
        Task<GetArchivedWebsiteQuery> GetAsync(Guid publicId, CancellationToken cancellationToken = default);
        Task<GetArchivedWebsiteQuery> GetAsync(string shortId, CancellationToken cancellationToken = default);
        Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(CancellationToken cancellationToken = default);

        Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default);
    }
}
