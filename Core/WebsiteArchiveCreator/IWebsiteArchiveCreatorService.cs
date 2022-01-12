using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Domain.Enums;

namespace Core.WebsiteArchiveCreator;

public interface IWebsiteArchiveCreatorService
{
    Task<(Status status, string shortId)> CreateAsync(CreateArchivedWebsiteCommand command, Guid userPublicId, CancellationToken cancellationToken = default);
    Task<(Status status, string shortId)> UploadAsync(string shortId, Stream stream, CancellationToken cancellationToken = default);
    Task DeactivateAsync(Guid publicId, Guid userPublicId, CancellationToken cancellationToken = default);
}