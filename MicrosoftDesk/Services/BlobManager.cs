using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Xamarin.Forms;

namespace MicrosoftHelpDesk
{
    public class BlobManager
    {

        public static BlobManager Instance
        {
            get { return new BlobManager(); }
        }


        BlobManager()
        {
            _fullResContainer = _blobClient.GetContainerReference("helpdeskblob");
            _lowResContainer = _blobClient.GetContainerReference("lowres");

        }

        CloudBlobClient _blobClient = CloudStorageAccount
            .Parse(connectionString)
            .CreateCloudBlobClient();

        static string connectionString = "DefaultEndpointsProtocol=https;AccountName=helpdeskblob;AccountKey=5txUqVmv4CuriHFsGMEY7P+jWjuOeLHPvq4Af0g7XKTdmGt1K0ruZhBPrH5Vt0OkQNT2+XqmJ+JBg8b0MDs4WQ==;EndpointSuffix=core.windows.net";

        CloudBlobContainer _lowResContainer;
        CloudBlobContainer _fullResContainer;

        public async Task<List<Uri>> GetAllBlobUrisAsync()
        {
            var contToken = new BlobContinuationToken();
            var allBlobs = await _fullResContainer.ListBlobsSegmentedAsync(contToken).ConfigureAwait(false);
            var uris = allBlobs.Results.Select(b => b.Uri).ToList();
            return uris;
        }

        public async Task<Uri> UploadFileAsync(string localPath)
        {
            var uniqueBlobName = Guid.NewGuid().ToString();
            uniqueBlobName += Path.GetExtension(localPath);
            var blobRef = _fullResContainer.GetBlockBlobReference(uniqueBlobName);

            await blobRef.UploadFromFileAsync(localPath).ConfigureAwait(false);

            return blobRef.Uri;

        }
    }
}
