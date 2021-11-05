using Core.Database;
using Core.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Reader
{
    public class ReaderService : IReaderService
    {
        private readonly AreawaDbContext _areawaDbContext;

        public ReaderService(AreawaDbContext areawaDbContext)
        {
            _areawaDbContext = areawaDbContext;
        }

        public async Task<QueryResult<GetArchivedWebsiteQuery>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default)
        {
            return new QueryResult<GetArchivedWebsiteQuery>
            {
                AllCount = await _areawaDbContext.WebsiteArchive
                    .AddCustomFilters(filterQuery, false)
                    .CountAsync(cancellationToken),
                Items = (await _areawaDbContext.WebsiteArchive
                    .AddCustomFilters(filterQuery)
                    .ToListAsync(cancellationToken))
                    .ConvertAll(x => x.Map())
            };
        }
    }
}
