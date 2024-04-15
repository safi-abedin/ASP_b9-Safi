using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application.Features.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IAmazonS3 _s3Client;


        public PhotoService()
        {
            RegionEndpoint regionEndpoint = RegionEndpoint.USEast1;

            _s3Client = new AmazonS3Client(regionEndpoint);
        }


        public async Task<PutObjectResponse> UploadFile(IFormFile photo)
        {
            var request = new PutObjectRequest
            {
                BucketName = "safi-bucket",
                Key = photo.FileName,
                InputStream = photo.OpenReadStream()
            };
            request.Metadata.Add("Content-Type",photo.ContentType);
            var response = await _s3Client.PutObjectAsync(request);

            return response;
        }

        public async Task<string> GetPhotoAsync(string? key)
        {

            var url = await _s3Client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
            {
                BucketName= "safi-bucket",
                Key=key,
                Expires = DateTime.UtcNow.AddMinutes(2)
            });

            return url;
        }

    }
}
