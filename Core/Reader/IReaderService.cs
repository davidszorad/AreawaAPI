using Core.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Reader
{
    public interface IReaderService
    {
        Task<ArchivedWebsite> GetAsync(Guid publicId, CancellationToken cancellationToken = default);
        Task<ArchivedWebsite> GetAsync(string shortId, CancellationToken cancellationToken = default);
        Task<QueryResult<ArchivedWebsite>> GetAsync(CancellationToken cancellationToken = default);
    }
}
