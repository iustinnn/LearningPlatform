using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace LearningPlatform.App.Contracts
{
    public interface IAzureDataService
    {
        Task<List<BlobUploadResultDto>> UploadFiles(List<IBrowserFile> files);
        Task<List<BlobItem>> GetBlobs();

    }
}
