using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BirdWatcherG2.Services
{
	public class BlobService
	{
		// we create a readonly instance of the blobserviceclient
		// (we don't want it to be modifyable once it has been created)
		private readonly BlobServiceClient blobServiceClient;
		// we then define the name of the container (basically a folder)
		// inside our blob storage
		private const string containerName = "images";

		// constructor is used to initialize a class
		public BlobService(string connectionString) {
			// option 1
			blobServiceClient = new(connectionString);
			// option 2
			blobServiceClient = new BlobServiceClient(connectionString);
		}

		// we make it a TASK so it can run in the background without making
		// the rest of the app wait for it to finish uploading
		public async Task<string> UploadImageAsync(IFormFile imageToUpload) {
			// "opening" the folder we want to store the image in, inside the blob
			var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
			// create the folder if it doesn't already exist
			await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
			// create the file name to upload
			var blobClient = containerClient.GetBlobClient(Guid.NewGuid().ToString() +
			Path.GetExtension(imageToUpload.FileName));
			// now we need to open the image we want to upload, convert it to a stream of data,
			// then upload it
			using var stream = imageToUpload.OpenReadStream();
			// upload time
			await blobClient.UploadAsync(stream, true);
			// return the url so we can store it in the database
			return blobClient.Uri.ToString();


		}

	}
}
