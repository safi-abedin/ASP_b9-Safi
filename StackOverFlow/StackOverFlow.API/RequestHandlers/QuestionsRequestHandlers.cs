using Autofac;
using MailKit.Search;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure;

namespace StackOverFlow.API.RequestHandlers
{
    public class QuestionsRequestHandlers : DataTables
    {
        private IQuestionManagementService? _questionManagementService;
        private ILifetimeScope _scope;
       

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string TriedApproach { get; set; }

        public Guid? UserId { get; set; }

        public List<string> Tags { get; set; }

        //Display property
        public List<SelectListItem>? MultiTags { get; set; }

        public QuestionsRequestHandlers()
        {
            
        }

        public QuestionsRequestHandlers(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }


        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        internal async Task CreateQuestionAsync()
        {
            var body = Details + TriedApproach ;
            var userId = UserId;
            var s = userId;
            var selectedTags = new List<Tag>();
            foreach(var tag in Tags)
            {
                selectedTags.Add(new Tag {Id=Guid.NewGuid(),Name=tag});
            }

            //await _questionManagementService.CreateQuestionAsync(Title, body, selectedTags);
        }



        internal async Task<object?> GetPagedCourses()
        {

            var data = await _questionManagementService?.GetPagedQuestionsAsync(
                PageIndex,
                PageSize,
                FormatSortExpression("Title", "Description", "Fees"));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.title,
                                record.Body,
                                record.Tags.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _questionManagementService.GetQuestionsAsync();
        }
    }
}
