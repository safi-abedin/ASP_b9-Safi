using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Application.Features.Question;

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
            await _questionManagementService.CreateQuestionAsync(Title, body, Tags);
        }

        internal void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }
    }
}
