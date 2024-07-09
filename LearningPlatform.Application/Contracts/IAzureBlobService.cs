using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace LearningPlatform.Application.Contracts
{
    public interface IAzureBlobService
    {
        Task<List<BlobUploadResultDto>> UploadFiles(List<IFormFile> files);

        Task<List<BlobItem>> GetBlob();
    }
}
