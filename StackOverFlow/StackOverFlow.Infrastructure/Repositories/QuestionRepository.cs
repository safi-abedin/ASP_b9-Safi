using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            Func<IQueryable<Question>, IIncludableQueryable<Question, object>> include = query =>
                query.Include(q => q.Tags);

            var data = await GetDynamicAsync(expression,null,include, pageIndex, pageSize, true);


            return data;
        }


        public async Task<IList<Question>> GetAllQuestionsAsync()
        {
            return await _dbSet.Include(q => q.Tags).ToListAsync();
        }

        public async Task<Question> GetAsync(Guid id)
        {
            Func<IQueryable<Question>, IIncludableQueryable<Question, object>> include = query =>
               query.Include(q => q.Tags).Include(a=>a.Answers);

            Expression<Func<Question, bool>> expression = null;

            if (!id.Equals(null))
            {
                expression = x => x.Id == id;
            }
            var data = await GetAsync(expression,include);

            return data.First();
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetQuestionsByUser(int pageIndex, int pageSize, Guid userId)
        {
            Expression<Func<Question, bool>> expression = x=>x.CreatorUserId==userId;

            Func<IQueryable<Question>, IIncludableQueryable<Question, object>> include = query =>
                query.Include(q => q.Tags);

            var data = await GetDynamicAsync(expression, null, include, pageIndex, pageSize, true);


            return data;
        }

        public  async Task<IList<Question>> GetTagedQuestions(Guid TagId)
        {
            Func<IQueryable<Question>, IIncludableQueryable<Question, object>> include = query =>
              query.Include(q => q.Tags);

            Expression<Func<Question, bool>> expression = q => q.Tags.Any(t => t.Id == TagId);

            var data = await GetAsync(expression, include);

            return data;
        }
    }
}
