using Autofac;
using AutoMapper;
using StackOverFlow.Application.Features.Questions;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class AnswerCreateModel
    {
        public Guid QuestionId { get; set; }

        public string AnswerBody { get; set; }


        public Guid UserID {  get; set; }

        private ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;



        private IMapper _mapper;


        public AnswerCreateModel()
        {

        }

        public AnswerCreateModel(IQuestionManagementService questionManagementService, IMapper mapper)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
        }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        internal async Task CreateAnswerAsync()
        {
            await _questionManagementService.CreateAnswerAsync(QuestionId, AnswerBody,UserID);
        }
    }
}
