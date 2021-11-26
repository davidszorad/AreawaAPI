using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.WebsiteArchiveCreator;

public interface IWebsiteArchiveCreatorService
{
    Task<string> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, CancellationToken cancellationToken = default);
}