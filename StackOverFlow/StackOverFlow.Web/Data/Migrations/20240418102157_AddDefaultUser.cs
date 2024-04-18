using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverFlow.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AboutMe", "AccessFailedCount", "ConcurrencyStamp", "DisplayName", "Email", "EmailConfirmed", "FirstName", "LastName", "Location", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureUrl", "Reputation", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("4e91c117-5b83-4faa-96cf-22874e306e42"), null, 0, "35b3c677-9b04-4df5-ad6a-a90098830a62", "User2", "user2@NoClaim.com", true, "Jane", "Smith", "Manchester", true, null, "USER2@NOCLAIM.COM", "USER2", "AQAAAAIAAYagAAAAEJfHIKHJ79TdXns/xUlhbnPmgI7HhtHKyhgWmE9JpD2o1pXUGl7FVqvdJ/cmesfwdg==", "9876543210", true, null, 0, "2711f32b-461d-4d51-a330-35e0daa39fa5", false, "user2" },
                    { new Guid("5e2eadd0-1132-44b0-ad9e-d005f777374e"), null, 0, "f2c329f7-f548-4797-b8c9-428ccc98d182", "User1", "user1@AllClaim.com", true, "John", "Doe", "London", true, null, "USER1@ALLCLAIM.COM", "USER1", "AQAAAAIAAYagAAAAECj4uFAaFDQHaIGCcc+f/DoNDaya3ZvYYPSgEtmCYI0jw7RWTBF1esIse/wtFo95yw==", "1234567890", true, null, 0, "ca0ce49a-7414-4c9f-b10c-66c936487c2b", false, "user1" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4e91c117-5b83-4faa-96cf-22874e306e42"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5e2eadd0-1132-44b0-ad9e-d005f777374e"));
        }
    }
}
