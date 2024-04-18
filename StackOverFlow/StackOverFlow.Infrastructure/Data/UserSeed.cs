using Microsoft.AspNetCore.Identity;
using StackOverFlow.Infrastructure.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Infrastructure.Data
{
    public class UserSeed
    {
        public ApplicationUser[] users { get; set; }


        public UserSeed()
        {

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            string password = "user1@2024";
            string passwordHashed = passwordHasher.HashPassword(null, password);
            string password2 = "user2@2024";
            string passwordHashed2 = passwordHasher.HashPassword(null, password2);

            users = new ApplicationUser[]
                     {
                 new ApplicationUser
                 {
                     Id = Guid.NewGuid(),
                     UserName = "user1",
                     Email = "user1@AllClaim.com",
                     NormalizedUserName = "USER1",
                     NormalizedEmail = "USER1@ALLCLAIM.COM",
                     EmailConfirmed = true,
                     PasswordHash = passwordHashed, 
                     SecurityStamp = Guid.NewGuid().ToString(),
                     PhoneNumber = "1234567890",
                     PhoneNumberConfirmed = true,
                     TwoFactorEnabled = false,
                     LockoutEnd = null,
                     LockoutEnabled = true,
                     AccessFailedCount = 0,
                     FirstName = "John",
                     LastName = "Doe",
                     ProfilePictureUrl = null,
                     AboutMe =null ,
                     DisplayName = "User1",
                     Location = "London",
                     Reputation = 0
                 },
                 new ApplicationUser
                 {
                     Id = Guid.NewGuid(),
                     UserName = "user2",
                     Email = "user2@NoClaim.com",
                     NormalizedUserName = "USER2",
                     NormalizedEmail = "USER2@NOCLAIM.COM",
                     EmailConfirmed = true,
                     PasswordHash = passwordHashed2,
                     SecurityStamp = Guid.NewGuid().ToString(),
                     PhoneNumber = "9876543210",
                     PhoneNumberConfirmed = true,
                     TwoFactorEnabled = false,
                     LockoutEnd = null,
                     LockoutEnabled = true,
                     AccessFailedCount = 0,
                     FirstName = "Jane",
                     LastName = "Smith",
                     ProfilePictureUrl = null,
                     AboutMe = null,
                     DisplayName = "User2",
                     Location = "Manchester",
                     Reputation = 0
                 }
        };
        }

    }
}
