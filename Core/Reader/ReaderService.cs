using Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Reader
{
    public class ReaderService : IReaderService
    {
        public async Task<GetArchivedWebsiteQuery> GetAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            return GetFakes().First();
        }

        public async Task<GetArchivedWebsiteQuery> GetAsync(string shortId, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            return GetFakes().First(x => x.ShortId == shortId);
        }

        public async Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            var result = new QueryResult<GetArchivedWebsiteQuery>();
            result.AllCount = GetFakes().Count;
            result.Items = GetFakes();
            return result;
        }

        public async Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default)
        {
            return await GetAsync(cancellationToken).ConfigureAwait(false);
        }

        private ICollection<GetArchivedWebsiteQuery> GetFakes()
        {
            var item1 = new GetArchivedWebsiteQuery
            {
                PublicId = Guid.NewGuid(),
                ShortId = "XXX-XXX-XXX",
                ArchiveType = ArchiveType.Pdf,
                ArchiveUrl = "none",
                SourceUrl = "none",
                Status = Status.Ok,
                Created = DateTime.UtcNow,
                Description = "Description 1",
                Name = "Name 1",
                Updated = DateTime.UtcNow
            };
            var item2 = new GetArchivedWebsiteQuery
            {
                PublicId = Guid.NewGuid(),
                ShortId = "YYY-YYY-YYY",
                ArchiveType = ArchiveType.Pdf,
                ArchiveUrl = "none",
                SourceUrl = "none",
                Status = Status.Pending,
                Created = DateTime.UtcNow,
                Description = "Description 2",
                Name = "Name 2",
                Updated = DateTime.UtcNow
            };
            return new List<GetArchivedWebsiteQuery>
            {
                item1,
                item2
            };
        }
    }
}
