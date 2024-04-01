using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Repositories
{
    public interface IQuestionRepository : IRepositoryBase<Question, Guid>
    {
        Task<(IList<Question> records, int total, int totalDisplay)> GetTableDataAsync(string orderBy, int pageIndex, int pageSize);

        Task<IList<Question>> GetAllQuestionsAsync();
    }
}
