using Amazon.S3.Model;
using Autofac;
using AutoMapper;
using StackOverFlow.Application.Features.Photos;
using StackOverFlow.Application.Features.Questions;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class ProfileEditModel
    {

        public ILifetimeScope _scope;


        public IPhotoService _photoService;

        public Guid UserId {  get; set; }

        public string? DisplayName { get; set; }


        public string? AboutMe { get; set; }


        public string? Location { get; set; }


        public int? Reputation { get; set; }

        public string? ProfilePictureUrl { get; set; }


        public IFormFile Photo { get; set; }


        public ProfileEditModel()
        {
            
        }

        public ProfileEditModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        internal void ResolveAsync(ILifetimeScope scope)
        {
            _scope = scope;
            _photoService = _scope.Resolve<IPhotoService>();
        }

        internal async Task<PutObjectResponse> UploadFile(IFormFile photo)
        {
           return await _photoService.UploadFile(photo);
        }
    }
}
