using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Configuration;
using Core.Shared;
using Domain.Enums;

namespace Infrastructure;

public class AzureBlobStorageService : IStorageService
{
    public async Task<string> UploadAsync(Stream stream, ArchiveType archiveType, string folder, string file, CancellationToken cancellationToken = default)
    {
        var containerClient = await CreateContainerAsync(folder.ToLower(), cancellationToken);
        BlobClient blobClient = containerClient.GetBlobClient(file.ToLower());
        
        var blobUploadOptions = new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = GetContentType(archiveType)
            }
        };

        await blobClient.UploadAsync(stream, blobUploadOptions, cancellationToken);
        return $"{folder.ToLower()}/{file.ToLower()}";
    }

    public async Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default)
    {
        string connectionString = ConfigStore.GetValue(ConfigurationConstants.AzureStorageConnectionString);
        var blobServiceClient = new BlobServiceClient(connectionString);
        await blobServiceClient.DeleteBlobContainerAsync(folder.ToLower(), cancellationToken: cancellationToken);
    }

    private async Task<BlobContainerClient> CreateContainerAsync(string containerName, CancellationToken cancellationToken = default)
    {
        string connectionString = ConfigStore.GetValue(ConfigurationConstants.AzureStorageConnectionString);
        var blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.Blob, cancellationToken: cancellationToken);
        return containerClient;
    }
    
    private string GetContentType(ArchiveType archiveType)
    {
        return archiveType switch
        {
            ArchiveType.Pdf => "application/pdf",
            ArchiveType.Png => "image/png",
            _ => throw new ArgumentOutOfRangeException(nameof(archiveType), archiveType, null)
        };
    }
}