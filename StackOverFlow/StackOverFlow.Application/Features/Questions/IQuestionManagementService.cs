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
        Task CreateQuestionAsync(string title, string body, List<string> tags,Guid UserId);
        Task EditAsync(Guid id, string title, string Body, List<string> tags);
        Task<IEnumerable<Tag>> GetAllTags();
        Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAsync(int pageIndex, int pageSize, string orderBy);
        Task<Question> GetQuestionAsync(Guid id);
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}
