using Autofac;
using StackOverFlow.Application.Features.Questions;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionVoteModel
    {
        private ILifetimeScope _scope;
        private IQuestionManagementService _questionManagementService;


        public QuestionVoteModel()
        {
        }

        public QuestionVoteModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        internal async Task<bool> CheckVoteAsync(Guid QuestionId, Guid userId)
        {
           return await _questionManagementService.CheckVote(QuestionId, userId);
        }

        internal async Task GiveDownVoteAsync(Guid QuestionId, Guid userId)
        {
             await _questionManagementService.GiveDownVote(QuestionId, userId);
        }
    }
}
