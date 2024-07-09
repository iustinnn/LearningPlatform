namespace LearningPlatform.Application.Contracts
{
    public class BlobUploadResultDto
    {
        public string BlobName { get; set; }
        public string ContainerName { get; set; }
        public string Uri { get; set; }
        public string ETag { get; set; }
        public DateTimeOffset LastModified { get; set; }
    }
}