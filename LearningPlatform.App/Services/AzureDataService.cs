using Azure.Storage.Blobs.Models;
using LearningPlatform.App.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace LearningPlatform.App.Services
{
    public class AzureDataService : IAzureDataService
    {
        private readonly HttpClient _httpClient;

        public AzureDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BlobUploadResultDto>> UploadFiles(List<IBrowserFile> files)
        {
            var content = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var streamContent = new StreamContent(file.OpenReadStream(long.MaxValue));
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(streamContent, "files", file.Name);
            }

            var response = await _httpClient.PostAsync("api/v1/AzureBlob", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
       
            using (var document = JsonDocument.Parse(responseContent))
            {
                var root = document.RootElement;
                var result = new List<BlobUploadResultDto>();

                foreach (var element in root.EnumerateArray())
                {
                    var dto = new BlobUploadResultDto
                    {
                        BlobName = element.GetProperty("blobName").GetString(),
                        ContainerName = element.GetProperty("containerName").GetString(),
                        Uri = element.GetProperty("uri").GetString(),
                        ETag = element.GetProperty("eTag").GetString(),
                        LastModified = element.GetProperty("lastModified").GetDateTimeOffset()
                    };

                    result.Add(dto);
                }

                return result;
            }
        }

        public async Task<List<BlobItem>> GetBlobs()
        {
            var response = await _httpClient.GetAsync("api/azureBlob/BlobList");
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<BlobItem>>(responseContent);
        }
    
    }
}
