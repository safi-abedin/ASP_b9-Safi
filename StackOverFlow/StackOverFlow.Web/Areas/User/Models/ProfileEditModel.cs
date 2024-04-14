namespace StackOverFlow.Web.Areas.User.Models
{
    public class ProfileEditModel
    {

        public Guid UserId {  get; set; }

        public string? DisplayName { get; set; }


        public string? AboutMe { get; set; }


        public string? Location { get; set; }


        public int? Reputation { get; set; }

        public string? ProfilePictureUrl { get; set; }
    }
}
