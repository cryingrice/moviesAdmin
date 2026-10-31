using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MoviesAdmin.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IConfiguration configuration)
        {
            // Container names must be lowercase and meet Azure rules. Normalize here to avoid InvalidResourceName errors.
            var rawContainer = configuration["whereamievenallowed"]
                ?? throw new InvalidOperationException("whereamievenallowed is missing from appsettings.");
            _containerName = rawContainer.Trim().ToLowerInvariant();

            string connectionString = configuration.GetConnectionString("whereamievenallowed")
                ?? throw new InvalidOperationException("whereamievenallowed is missing from ConnectionStrings.");

            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);

            // Ensure the container exists. Create it without enabling public access
            // because some storage accounts disallow public access at the account level.
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(filename);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            return blobClient.Uri.ToString();
        }
    }
}