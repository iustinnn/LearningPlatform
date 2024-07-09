using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LearningPlatform.Application.Contracts;
using Microsoft.AspNetCore.Http;
namespace LearningPlatform.Infrastructure.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        BlobServiceClient _blobServiceClient;
        BlobContainerClient _containerClient;
        string azureConnectionString = "DefaultEndpointsProtocol=https;AccountName=stlearnplatform;AccountKey=taBOuUDLFL7eB6RVxVabutgBh6gvEw4BZ2Awpp5Yo7CIxLfVOcjkdRnc8+ILsuJM+yhgXCaTDdkF+AStBKllmg==;EndpointSuffix=core.windows.net";

        public AzureBlobService()
        {
            _blobServiceClient = new BlobServiceClient(azureConnectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient("stplatformcontainer");
        }

        public async Task<List<BlobUploadResultDto>> UploadFiles(List<IFormFile> files)
        {
            var uploadResults = new List<BlobUploadResultDto>();
            foreach (var file in files)
            {
                var blobClient = _containerClient.GetBlobClient(file.FileName);

                using var stream = file.OpenReadStream();
                var response = await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });

                uploadResults.Add(new BlobUploadResultDto
                {
                    BlobName = blobClient.Name,
                    ContainerName = blobClient.BlobContainerName,
                    Uri = blobClient.Uri.ToString(),
                    ETag = response.Value.ETag.ToString(),
                    LastModified = response.Value.LastModified
                });
            }
            return uploadResults;
        }

        public async Task<List<BlobItem>> GetBlob()
        {
            var items = new List<BlobItem>();
            await foreach (var blobItem in _containerClient.GetBlobsAsync())
            {
                items.Add(blobItem);
            }
            return items;
        }
    }
}
