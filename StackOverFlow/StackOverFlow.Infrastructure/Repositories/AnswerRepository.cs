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

namespace StackOverFlow.Infrastructure.Repositories
{
    public class AnswerRepository : Repository<Answer, Guid>, IAnswerRepository
    {
        public AnswerRepository(IApplicationDbContext context) : base((DbContext)context)
        {

        }

        public async Task<Answer> GetAsync(Guid answerId)
        {

            Func<IQueryable<Answer>, IIncludableQueryable<Answer, object>> include = query =>
                            query.Include(v => v.AnswerVotes);

            Expression<Func<Answer, bool>> expression = null;

            if (!answerId.Equals(null))
            {
                expression = x => x.Id == answerId;
            }

            var data = await GetAsync(expression, include);

            return data.First();
        }

        public async Task<IList<Answer>> GetVoteAsync(Guid answerId, Guid userId)
        {
            Func<IQueryable<Answer>, IIncludableQueryable<Answer, object>> include = query =>
                           query.Include(v => v.AnswerVotes);

            Expression<Func<Answer, bool>> expression = null;

            if (answerId != Guid.Empty)
            {
                expression = x => x.Id == answerId && x.AnsweredByUserId == userId;
            }

            var data = await GetAsync(expression, include);

            return data;
        }

    }
}
