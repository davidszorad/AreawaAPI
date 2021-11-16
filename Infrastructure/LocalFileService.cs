using System;
using System.IO;
using Configuration;
using Core.Shared;
using Domain.Enums;

namespace Infrastructure;

internal class LocalFileService
{
    private readonly string _webRootPath;
    
    public LocalFileService(string webRootPath)
    {
        _webRootPath = webRootPath;
    }
    
    public string PrepareEmptyFile(ArchiveFile file)
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
    
    public void CleanUp(string filePath)
    {
        var directory = Path.GetDirectoryName(filePath);
        if (string.IsNullOrWhiteSpace(directory))
        {
            throw new ArgumentNullException($"{nameof(LocalFileService)} - filePath: {filePath}");
        }
        
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException($"{nameof(LocalFileService)} - {directory}");
        }
        Directory.Delete(directory, true);
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
}