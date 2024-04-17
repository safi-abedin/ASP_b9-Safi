using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StackOverFlow.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVoteTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      
            migrationBuilder.CreateTable(
                name: "AnswerVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotedBYId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Up = table.Column<bool>(type: "bit", nullable: false),
                    Down = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerVotes_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerVotes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QuestionVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VotedBYId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Up = table.Column<bool>(type: "bit", nullable: false),
                    Down = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionVotes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

          

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVotes_AnswerId",
                table: "AnswerVotes",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVotes_QuestionId",
                table: "AnswerVotes",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVotes_QuestionId",
                table: "QuestionVotes",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerVotes");

            migrationBuilder.DropTable(
                name: "QuestionVotes");

        }
    }
}
