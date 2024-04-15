using Amazon.S3.Model;
using Autofac;
using StackOverFlow.Application.Features.Photos;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class ProfileViewModel
    {

        public ILifetimeScope _scope;


        public IPhotoService _photoService;

        public string? DisplayName { get; set; }

        public string? ImageURL { get; set; }

        public string? AboutMe { get; set; }

        public ProfileViewModel()
        {
            
        }

        public ProfileViewModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        internal void ResolveAsync(ILifetimeScope scope)
        {
            _scope = scope;
            _photoService = _scope.Resolve<IPhotoService>();
        }

        internal async Task<string> GetPhotoAsync(string? key)
        {
            return await _photoService.GetPhotoAsync(key);
        }
    }
}
