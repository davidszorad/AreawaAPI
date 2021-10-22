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
        public async Task<ArchivedWebsite> GetAsync(Guid publicId, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            return GetFakes().First();
        }

        public async Task<ArchivedWebsite> GetAsync(string shortId, CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            return GetFakes().First(x => x.ShortId == shortId);
        }

        public async Task<QueryResult<ArchivedWebsite>> GetAsync(CancellationToken cancellationToken = default)
        {
            await Task.FromResult(0);
            var result = new QueryResult<ArchivedWebsite>();
            result.AllCount = GetFakes().Count;
            result.Items = GetFakes();
            return result;
        }

        private ICollection<ArchivedWebsite> GetFakes()
        {
            var item1 = new ArchivedWebsite
            {
                PublicId = Guid.NewGuid(),
                ShortId = "XXX-XXX-XXX",
                ArchiveType = ArchiveType.Pdf,
                ArchiveUrl = new Uri("none"),
                SourceUrl = new Uri("none"),
                Status = Status.Ok,
                Created = DateTime.UtcNow,
                Description = "Description 1",
                Name = "Name 1",
                Updated = DateTime.UtcNow
            };
            var item2 = new ArchivedWebsite
            {
                PublicId = Guid.NewGuid(),
                ShortId = "YYY-YYY-YYY",
                ArchiveType = ArchiveType.Pdf,
                ArchiveUrl = new Uri("none"),
                SourceUrl = new Uri("none"),
                Status = Status.Pending,
                Created = DateTime.UtcNow,
                Description = "Description 2",
                Name = "Name 2",
                Updated = DateTime.UtcNow
            };
            var items = new List<ArchivedWebsite>();
            items.Add(item1);
            items.Add(item2);
            return items;
        }
    }
}
