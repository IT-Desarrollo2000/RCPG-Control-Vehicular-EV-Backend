using Application.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.BlobStorage
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<List<string>> GetBlobList(string container)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            var blobs = blobContainer.GetBlobs();
            List<string> blobList = new List<string>();

            foreach (var blob in blobs)
            {
                blobList.Add(blob.Name);
            }

            return blobList;
        }

        public async Task<bool> BlobExists(string container, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            var blobClient = blobContainer.GetBlobClient(path);
            var exists = await blobClient.ExistsAsync();

            return exists;
        }

        public async Task<byte[]> GetFileByName(string container, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);

            var blobClient = blobContainer.GetBlobClient(path);
            var downloadContent = await blobClient.DownloadAsync();
            using (MemoryStream ms = new MemoryStream())
            {
                await downloadContent.Value.Content.CopyToAsync(ms);
                return ms.ToArray();
            }
        }

        public async Task<string> GetFileUrl(string container, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);

            var blobClient = blobContainer.GetBlobClient(path);
            var downloadContent = blobClient.Uri.AbsoluteUri;
            return downloadContent;
        }

        public async Task<string> UploadFileToBlobAsync(IFormFile file, string container, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            await blobContainer.CreateIfNotExistsAsync();

            var blobClient = blobContainer.GetBlobClient(path);

            await blobClient.UploadAsync(file.OpenReadStream());

            var fileUrl = blobClient.Uri.AbsoluteUri;

            return fileUrl;
        }

        public async Task DeleteFileFromBlobAsync(string container, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(container);
            await blobContainer.DeleteBlobIfExistsAsync(path, DeleteSnapshotsOption.IncludeSnapshots);
        }

        public async Task CreateContainerAsync(string containerName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            var exist = await blobContainer.ExistsAsync();

            if (!exist)
            {
                await _blobServiceClient.CreateBlobContainerAsync(containerName);
            }
        }

        public async Task DeletBlobContainerAsync(string containerName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(containerName);
            var exist = await blobContainer.ExistsAsync();

            if (exist)
            {
                await _blobServiceClient.DeleteBlobContainerAsync(containerName);
            }
        }

        public async Task RestoreBlobContainerAsync(string containerName, string version)
        {
            await _blobServiceClient.UndeleteBlobContainerAsync(containerName, version);
        }
    }
}
