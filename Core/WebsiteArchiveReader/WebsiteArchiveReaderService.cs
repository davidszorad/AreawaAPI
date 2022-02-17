using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.WebsiteArchiveReader;

public class WebsiteArchiveReaderService : IWebsiteArchiveReaderService
{
    private readonly AreawaDbContext _areawaDbContext;

    public WebsiteArchiveReaderService(AreawaDbContext areawaDbContext)
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