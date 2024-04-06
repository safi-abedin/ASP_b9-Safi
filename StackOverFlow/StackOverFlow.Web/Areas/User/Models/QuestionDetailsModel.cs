using Autofac;
using AutoMapper;
using StackOverFlow.Application.Features.Questions;
using System.ComponentModel.DataAnnotations;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionDetailsModel
    {
        public Guid Id { get; set; }

        public string title { get; set; }

        public string Body { get; set; }


        public Guid CreatorUserId { get; set; }


        public DateTime CreationDateTime { get; set; }

        public int ViewCount { get; set; }


        public int VoteCount { get; set; }

        public int AnswerCount { get; set; }

        private ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;


        private IMapper _mapper;


        public QuestionDetailsModel()
        {

        }

        public QuestionDetailsModel(IQuestionManagementService questionManagementService, IMapper mapper)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
        }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            var question = await _questionManagementService.GetQuestionAsync(id);
            
            if(question != null)
            {
                Id = question.Id;
                title = question.title;
                Body = question.Body;
                CreationDateTime = question.CreationDateTime;
                CreatorUserId = question.CreatorUserId;
                ViewCount = question.ViewCount;
                VoteCount = question.VoteCount;
                AnswerCount = question.AnswerCount;
            }
        }

    }
}
