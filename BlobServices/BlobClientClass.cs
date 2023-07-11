using Azure.Storage.Blobs;

namespace BlobServices
{
    public class BlobClientClass
    {
        //Get blob resource from the client
        public BlobClient GetBlobClient(BlobServiceClient blobServiceClient,string containerName,string blobName)
        {
            BlobClient client =blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
            return client;
        }

        //Get blob resource from the container
        public BlobClient GetBlobClient(BlobContainerClient blobContainerClient, string blobName)
        {
            BlobClient client =blobContainerClient.GetBlobClient(blobName);
            return client;
        }
    }
}
