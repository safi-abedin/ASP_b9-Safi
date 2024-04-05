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
        Task CreateQuestionAsync(string title, string body, List<string> tags,Guid UserId);
        Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAsync(int pageIndex, int pageSize, string orderBy);
        Task<IEnumerable<Question>> GetQuestionsAsync();
    }
}
