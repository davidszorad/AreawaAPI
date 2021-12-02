using System.Threading;
using System.Threading.Tasks;
using Core.ChangeTracker.Extensions;
using Core.Shared;

namespace Core.ChangeTracker;

public class ChangeTrackerService : IChangeTrackerService
{
    private readonly IHttpService _httpService;

    public ChangeTrackerService(IHttpService httpService)
    {
        _httpService = httpService;
    }
    
    public async Task<string> GetSourcePreviewAsync(SourcePreviewCommand command, CancellationToken cancellationToken = default)
    {
        var htmlSource = await _httpService.GetHtmlSourceAsync(command.Url, cancellationToken);
        
        return htmlSource
            .StripHead()
            .StripScripts()
            .StripStyles();
    }
}