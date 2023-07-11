using Azure.Identity;
using Azure.Storage.Blobs;

namespace BlobServices
{
    public class BlobServiceClientClass
    {
        public BlobServiceClient GetBlobServiceClient(string accountName)
        {
            BlobServiceClient client = new(
                new Uri($"https://{accountName}.blob.core.windows.net"),
                new DefaultAzureCredential());

            return client;
        }
    }
}
