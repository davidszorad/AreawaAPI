using System;
using Core.Shared;
using Domain.Enums;

namespace Core.WebsiteArchiveReader;

public class GetArchivedWebsiteQuery : BaseItemResult
{
    public Guid PublicId { get; set; }
    public string ShortId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public string SourceUrl { get; set; }
    public string ArchiveUrl { get; set; }
    public ArchiveType ArchiveType { get; set; }
}