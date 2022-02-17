using System.Threading;
using System.Threading.Tasks;
using Core.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.WatchDogReader;

public class WatchDogReaderService : IWatchDogReaderService
{
    private readonly AreawaDbContext _areawaDbContext;

    public WatchDogReaderService(AreawaDbContext areawaDbContext)
    {
        _areawaDbContext = areawaDbContext;
    }
    
    public async Task<QueryResult<WatchDogQueryResult>> GetAsync(FilterQuery filterQuery, CancellationToken cancellationToken = default)
    {
        return new QueryResult<WatchDogQueryResult>
        {
            AllCount = await _areawaDbContext.WatchDog
                .AddCustomFilters(filterQuery, false)
                .CountAsync(cancellationToken),
            Items = (await _areawaDbContext.WatchDog
                    .AddCustomFilters(filterQuery)
                    .ToListAsync(cancellationToken))
                .ConvertAll(x => x.Map())
        };
    }
}