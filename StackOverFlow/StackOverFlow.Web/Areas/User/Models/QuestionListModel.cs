using Autofac;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Infrastructure;
using System.Web;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionListModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;


        public QuestionListModel()
        {
        }

        public QuestionListModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        public async Task<object> GetPagedCoursesAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionManagementService.GetPagedCoursesAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.GetSortText(new string[] { "", "", "" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                HttpUtility.HtmlEncode(record.title),
                                HttpUtility.HtmlEncode(record.Body),
                                record.Tags.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }


        /*internal async Task DeleteCourseAsync(Guid id)
        {
            await _questionManagementService..DeleteCourseAsync(id);
        }*/
    }
}
