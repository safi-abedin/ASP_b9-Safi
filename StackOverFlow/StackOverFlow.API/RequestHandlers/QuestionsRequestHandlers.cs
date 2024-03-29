using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure.Membership;

namespace StackOverFlow.API.RequestHandlers
{
    public class QuestionsRequestHandlers
    {
        private IQuestionManagementService? _questionManagementService;
        private ILifetimeScope _scope;
       

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string TriedApproach { get; set; }

        public Guid UserId { get; set; }

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

            await _questionManagementService.CreateQuestionAsync(Title, body, selectedTags);
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }
    }
}
