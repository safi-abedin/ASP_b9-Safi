using Autofac;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Infrastructure;
using System.Web;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class TagListModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;


        public TagListModel()
        {
        }

        public TagListModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }


        internal async Task<object> GetPagedTagsAsync(DataTablesAjaxRequestUtility dataTablesUtility)
        {
            var data = await _questionManagementService.GetPagedTagsAsync(
                dataTablesUtility.PageIndex,
                dataTablesUtility.PageSize,
                null);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new object[]{
                                record.Name.ToString(),
                                record.Description.ToString(),
                                record.Id.ToString()
                       }
            ).ToArray()
            };
        }
    }
}
