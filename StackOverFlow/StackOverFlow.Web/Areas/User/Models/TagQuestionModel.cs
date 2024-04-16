using Autofac;
using AutoMapper;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class TagQuestionModel
    {

        public string tagName { get; set; }
        public int TotalCount { get; set; }


        public IList<Question> Questions { get; set; }

        private ILifetimeScope _scope;

        private IQuestionManagementService _questionManagementService;

        private IMapper _mapper;




        public TagQuestionModel()
        {
        }

        public TagQuestionModel(IQuestionManagementService questionManagementService,IMapper mapper)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
            Questions = new List<Question>();

        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
            _mapper = _scope.Resolve<IMapper>();
        }


        internal async Task GetTagedQuestonsAsync(Guid id)
        {
            var questions = await _questionManagementService.GetTagedQuestionsAsync(id);

            if(questions is not null)
            {
                Questions = questions;
                TotalCount = questions.Count;
            }
            
        }
    }
}
