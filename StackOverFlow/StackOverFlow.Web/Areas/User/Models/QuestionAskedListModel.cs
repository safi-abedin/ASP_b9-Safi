using Autofac;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Infrastructure;
using System.Web;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionAskedListModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;

        public Guid UserId { get; set; }


        public QuestionAskedListModel()
        {
        }

        public QuestionAskedListModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        public async Task<object> GetPagedQuestionsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionManagementService.GetPagedQuestionsAskedAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,UserId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new object[]{
                                record.VoteCount.ToString(),
                                HttpUtility.HtmlEncode(record.AnswerCount),
                                record.ViewCount.ToString(),
                                HttpUtility.HtmlEncode(record.title),
                                (from tag in record.Tags select new string[] {
                                    tag.Name
                                }).ToArray(),
                                record.Id.ToString()
                       }
                ).ToArray()
            };
        }
    }
}
