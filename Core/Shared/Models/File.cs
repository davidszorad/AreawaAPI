using System;
using System.IO;
using Domain.Enums;

namespace Core.Shared;

public class ArchiveFile
{
    public string Filename { get; set; }
    public string Folder { get; set; }
    public string SourceUrl { get; set; }
    public ArchiveType Extension { get; set; }

    public string GetFilenameWithExtension()
    {
        return Extension switch
        {
            ArchiveType.Pdf => $"{Filename}.pdf",
            ArchiveType.Png => $"{Filename}.png",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}