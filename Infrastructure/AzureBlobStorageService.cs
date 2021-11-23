using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Configuration;
using Core.Shared;

namespace Infrastructure;

public class AzureBlobStorageService : IStorageService
{
    public async Task<string> UploadAsync(Stream stream, string folder, string file, CancellationToken cancellationToken = default)
    {
        var containerClient = await CreateContainerAsync(folder.ToLower(), cancellationToken);
        BlobClient blobClient = containerClient.GetBlobClient(file.ToLower());
        await blobClient.UploadAsync(stream, true, cancellationToken);
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
}