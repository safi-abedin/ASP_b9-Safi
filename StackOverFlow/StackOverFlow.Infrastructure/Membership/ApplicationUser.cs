using Microsoft.AspNetCore.Identity;
using System;

namespace StackOverFlow.Infrastructure.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? DisplayName { get; set; }


        public string? AboutMe { get; set; }


        public string? Location {  get; set; }


        public int? Reputation {  get; set; }
        


        public string? ProfilePictureUrl { get; set; }
    }
}
