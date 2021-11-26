using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Core.WebsiteArchiveCreator;

public interface IWebsiteArchiveCreatorService
{
    Task<string> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, Stream stream, CancellationToken cancellationToken = default);
}