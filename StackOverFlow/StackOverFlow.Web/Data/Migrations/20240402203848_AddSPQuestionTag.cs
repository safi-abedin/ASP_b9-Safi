using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackOverFlow.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSPQuestionTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER PROCEDURE [dbo].[GetAllQuestionsWithTag]
                            @PageIndex INT,
                            @PageSize INT,
                            @OrderBy NVARCHAR(50),
                            @Total INT OUTPUT,
                            @TotalDisplay INT OUTPUT
                        AS
                        BEGIN
                            SET NOCOUNT ON;

                            DECLARE @sql NVARCHAR(MAX);
                            DECLARE @paramList NVARCHAR(MAX); 
                            DECLARE @countsql NVARCHAR(MAX);
                            DECLARE @countparamList NVARCHAR(MAX);

                            SELECT @Total = COUNT(*) FROM Questions;

                            SET @countsql = 'SELECT @TotalDisplay = COUNT(*) FROM Questions q INNER JOIN QuestionsTags qt ON q.Id = qt.QuestionsId INNER JOIN Tags t ON qt.TagsId = t.Id WHERE 1 = 1';

                            IF @OrderBy IS NOT NULL
                                SET @countsql = @countsql + ' AND q.' + @OrderBy + ' IS NOT NULL';

                            SET @sql = 'SELECT q.Id AS QuestionId,
                                               q.title AS QuestionTitle,
                                               q.Body AS QuestionBody,
                                               q.CreatorUserId AS QuestionCreatorUserId,
                                               q.CreationDateTime AS QuestionCreationDateTime,
                                               t.Id AS TagId,
                                               t.Name AS TagName
                                        FROM Questions q
                                        INNER JOIN QuestionsTags qt ON q.Id = qt.QuestionsId
                                        INNER JOIN Tags t ON qt.TagsId = t.Id
                                        WHERE 1 = 1';

                            IF @OrderBy IS NOT NULL
                                SET @sql = @sql + ' AND q.' + @OrderBy + ' IS NOT NULL';

                            SET @sql = @sql + ' ORDER BY ' + @OrderBy + ' OFFSET @PageSize * (@PageIndex - 1) ROWS FETCH NEXT @PageSize ROWS ONLY';

                            EXEC sp_executesql @countsql, 
                                               N'@TotalDisplay INT OUTPUT',
                                               @TotalDisplay OUTPUT;

                            EXEC sp_executesql @sql,
                                               N'@PageSize INT, @PageIndex INT',
                                               @PageSize,
                                               @PageIndex;
                        END";
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE [dbo].[GetAllQuestionsWithTag]";
            migrationBuilder.Sql(sql);
        }
    }
}
