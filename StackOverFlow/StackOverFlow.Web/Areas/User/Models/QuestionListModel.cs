using Autofac;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure;
using System.Linq;
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

        public async Task<object> GetPagedQuestionsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionManagementService.GetPagedQuestionsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                dataTablesUtility.GetSortText(new string[] { "", "", "" }));

            var transformedData = data.records.Select(record => new
            {
                Title = HttpUtility.HtmlEncode(record.title),
                Body = HttpUtility.HtmlEncode(record.Body),
                Tags = record.Tags.Select(tag => tag.Name).ToList(), // Transform tags directly here
                Id = record.Id.ToString()
            });

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = transformedData
            };
        }


        /*internal async Task DeleteCourseAsync(Guid id)
        {
            await _questionManagementService..DeleteCourseAsync(id);
        }*/
    }
}
