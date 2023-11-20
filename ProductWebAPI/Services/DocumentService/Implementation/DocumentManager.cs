using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Minio;
using ProductWebAPI.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace ProductWebAPI.Services.DocumentService
{
    public class DocumentManager :IDocumentManager
    {
        #region globalfields
        private IConfiguration _configuration { get; }
        private readonly ILogger<DocumentManager> _logger;

        #endregion

        #region Constructors
        public DocumentManager(IConfiguration configuration, ILogger<DocumentManager> logger)
        {
            _logger = logger;
            this._configuration = configuration;
        }
        #endregion

        #region Methods
        public bool UpLoad(ProductImageModel productImage)
        {

            if (productImage.File == null)
                return false;

            var minioSection = _configuration.GetSection("MinIOSettings");

            var endpoint = minioSection["EndPoint"];
            var accessKey = minioSection["AccessKey"];
            var secretKey = minioSection["SecretKey"];
            var bucketName = minioSection["BucketName"];



            productImage.FileNameGuid = GenerateFileNameGuid(productImage.File);
            productImage.FileName = productImage.File.FileName;



            if (bucketName == null)
                return false;

            try
            {
                var minio = new MinioClient()
                                    .WithEndpoint(endpoint)
                                    .WithCredentials(accessKey, secretKey)
                                    .WithSSL()
                                    .Build();
                Run(minio, bucketName, productImage.FileNameGuid,productImage.File).Wait();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }

        }


        private async Task Run(IMinioClient minio,string bucketName,string fileNameGuid,IFormFile file)
        {

            try
            {
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }
                // Upload a file to bucket.

                using (var fileStream = new MemoryStream())
                {
                    file.CopyTo(fileStream);
                    var fileBytes = fileStream.ToArray();
                    var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(fileNameGuid)
                    .WithStreamData(new MemoryStream(fileBytes))
                    .WithObjectSize(fileStream.Length)
                    .WithContentType(file.ContentType);
                    await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);

                    _logger.LogInformation("Successfully uploaded " + file.FileName);
                }
            }
            catch (MinioException ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private static string GenerateFileNameGuid(IFormFile file)
        {
            var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
            return Guid.NewGuid().ToString() + Path.GetExtension(parsedContentDisposition.FileName.TrimStart('\"').TrimEnd('\"'));
        }

        #endregion
    }
}
