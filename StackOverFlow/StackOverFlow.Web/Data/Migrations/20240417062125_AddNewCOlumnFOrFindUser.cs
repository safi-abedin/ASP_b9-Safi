using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverFlow.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewCOlumnFOrFindUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "ReplyByEmail",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatorEmail",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnsweredByCreatorEmail",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            
            migrationBuilder.DropColumn(
                name: "ReplyByEmail",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "CreatorEmail",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnsweredByCreatorEmail",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Answers");

            
        }
    }
}
