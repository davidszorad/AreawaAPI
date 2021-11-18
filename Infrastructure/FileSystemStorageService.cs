using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Core.Shared;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure;

public class FileSystemStorageService : IStorageService
{
    private readonly IWebHostEnvironment _host;

    public FileSystemStorageService(IWebHostEnvironment host)
    {
        _host = host;
    }
    
    public async Task<string> UploadAsync(string screenshotPath, string folder, string file, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(Path.GetFullPath(folder)))
        {
            Directory.CreateDirectory(Path.GetFullPath(folder));
        }

        string fileName = Path.GetFileName(file);
        string filePath = Path.Combine(folder, fileName);
                
        File.Copy(screenshotPath, filePath, overwrite: true);

        return fileName;
    }
    
    public async Task<string> UploadAsync(Stream stream, string folder, string file, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(Path.GetFullPath(folder)))
        {
            Directory.CreateDirectory(Path.GetFullPath(folder));
        }

        string fileName = Path.GetFileName(file);
        string filePath = Path.Combine(folder, fileName);

        await using (var destinationFileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            while (stream.Position < stream.Length) 
            {
                destinationFileStream.WriteByte((byte)stream.ReadByte());
            }
        }

        return fileName;
    }

    public async Task DeleteFileAsync(string folder, string fileName, CancellationToken cancellationToken = default)
    {
        string fullPathWithoutFileName = Path.Combine(GetUploadsRootPath(), folder);

        if (!Directory.Exists(Path.GetFullPath(fullPathWithoutFileName)))
        {
            throw new DirectoryNotFoundException($"{nameof(FileSystemStorageService)} - {folder}");
        }

        string filePath = Path.Combine(fullPathWithoutFileName, fileName);

        File.Delete(filePath);
    }

    public async Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default)
    {
        string fullPath = Path.Combine(GetUploadsRootPath(), folder);

        if (!Directory.Exists(Path.GetFullPath(fullPath)))
        {
            throw new DirectoryNotFoundException($"{nameof(FileSystemStorageService)} - {folder}");
        }

        Directory.Delete(fullPath, recursive: true);
    }
    
    private string GetUploadsRootPath()
    {
        return _host.WebRootPath;
    }
}