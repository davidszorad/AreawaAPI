using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Core.WebsiteArchiveCreator;

public interface IWebsiteArchiveCreatorService
{
    Task<(Status status, string shortId)> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, Stream stream, CancellationToken cancellationToken = default);
}