using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBlobStorageService
    {
        Task<bool> BlobExists(string container, string path);
        Task CreateContainerAsync(string containerName);
        Task DeletBlobContainerAsync(string containerName);
        Task DeleteFileFromBlobAsync(string container, string path);
        Task<List<string>> GetBlobList(string container);
        Task<byte[]> GetFileByName(string container, string path);
        Task<string> GetFileUrl(string container, string path);
        Task RestoreBlobContainerAsync(string containerName, string version);
        Task<string> UploadFileToBlobAsync(IFormFile file, string container, string path);
    }
}
