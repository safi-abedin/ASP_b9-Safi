using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application.Features.Photos
{
    public interface IPhotoService
    {
        Task<Stream> GetPhotoAsync(string? key);
        Task<PutObjectResponse> UploadFile(IFormFile photo);
    }
}
