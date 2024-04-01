using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetTableDataAsync(string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Question, bool>> expression = null;

            
            return await GetDynamicAsync(expression,orderBy, null, pageIndex, pageSize, true);
        }

        public async Task<IList<Question>> GetAllQuestionsAsync()
        {
            return await _dbSet.Include(q => q.Tags).ToListAsync();
        }

    }
}
