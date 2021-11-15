using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Core.Shared;

namespace Infrastructure;

public class AzureBlobStorageService : IStorageService
{
    public async Task<string> CreateAsync(string folder, string file, CancellationToken cancellationToken = default)
    {
        // Docs - https://www.nuget.org/packages/Azure.Storage.Blobs
        
        // if (CloudStorageAccount.TryParse(settings.StorageSettings.AzureStorage, out CloudStorageAccount storageAccount))
        // {
        //     string fileName = Path.GetFileName(file);
        //     string filePath = Path.Combine(folder, fileName);
        //     
        //     var blobContainerClient = new BlobContainerClient(connectionString, containerName);//Use this client to perform operations on blob container.
        //     var blockBlobClient = blobContainerClient.GetBlockBlobClient(blobName);//Use this client to perform operations on block blob.
        //             
        //     CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
        //
        //     CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(folder);
        //     await cloudBlobContainer.CreateIfNotExistsAsync();
        //             
        //     // Set the permissions so the blobs are public. 
        //     BlobContainerPermissions permissions = new BlobContainerPermissions
        //     {
        //         PublicAccess = BlobContainerPublicAccessType.Blob
        //     };
        //     await cloudBlobContainer.SetPermissionsAsync(permissions);
        //
        //     CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
        //     if (file.Length > 0)
        //         await cloudBlockBlob.UploadFromFileAsync(file);
        //             
        //     return fileName;
        // }
        //throw new Exception(ExceptionConstants.ErrorConnectToCloudStorage);
        
        throw new System.NotImplementedException();
    }

    public async Task DeleteFileAsync(string folder, string fileName, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }

    public async Task DeleteFolderAsync(string folder, CancellationToken cancellationToken = default)
    {
        throw new System.NotImplementedException();
    }
}