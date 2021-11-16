using Domain.Enums;

namespace Core.Shared;

public class ArchiveFile
{
    public string Filename { get; set; }
    public string Folder { get; set; }
    public string SourceUrl { get; set; }
    public ArchiveType Extension { get; set; }
}