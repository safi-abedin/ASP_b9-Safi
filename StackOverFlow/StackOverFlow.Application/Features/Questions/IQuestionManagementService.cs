using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application.Features.Questions
{
    public interface IQuestionManagementService
    {
        Task CreateAnswerAsync(Guid questionId, string answerBody, Guid userID);
        Task CreateQuestionAsync(string title, string Details,string TriedApproach, List<string> tags,Guid UserId);
        Task DeleteQuestionAsync(Guid id);
        Task EditAsync(Guid id, string title, string Details,string TriedApproach, List<string> tags);
        Task<IEnumerable<Tag>> GetAllTags();
        Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAskedAsync(int pageIndex, int pageSize,Guid UserId);
        Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAsync(int pageIndex, int pageSize, string orderBy);
        Task<(IList<Tag> records, int total, int totalDisplay)> GetPagedTagsAsync(int pageIndex, int pageSize, string orderBy);
        Task<Question> GetQuestionAsync(Guid id);
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<Tag> GetTag(Guid id);
        Task<IList<Question>> GetTagedQuestionsAsync(Guid id);
        Task IncreaseView(Guid id);
    }
}
