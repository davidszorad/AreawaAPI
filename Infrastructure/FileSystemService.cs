using System;
using System.IO;
using Configuration;
using Core.Shared;
using Domain.Enums;

namespace Infrastructure;

internal class FileSystemService
{
    private readonly string _webRootPath;
    
    public FileSystemService(string webRootPath)
    {
        _webRootPath = webRootPath;
    }
    
    public string PrepareFile(ArchiveFile file)
    {
        var uploadsDirectory = CreateDirectory(file.Folder);
        var outputFile = Path.Combine(uploadsDirectory, Path.GetFileName($@"{file.Filename}{GetExtension(file.Extension)}"));
        var fileInfo = new FileInfo(outputFile);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        
        return outputFile;
    }

    private string GetExtension(ArchiveType fileExtension)
    {
        return fileExtension switch
        {
            ArchiveType.Pdf => ".pdf",
            ArchiveType.Png => ".png",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension, null)
        };
    }

    private string CreateDirectory(string folder)
    {
        var dir = Path.Combine(_webRootPath, ApplicationConstants.UPLOADS_ROOT, folder);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        return dir;
    }
    
    private void DeleteDirectory(string directory)
    {
        if (Directory.Exists(directory))
        {
            Directory.Delete(directory, true);
        }
    }
}