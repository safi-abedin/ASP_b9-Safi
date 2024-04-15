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
    public class TagRepository : Repository<Tag, Guid>, ITagRepository
    {
        public TagRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<(IList<Tag> records, int total, int totalDisplay)> GetTableTagsAsync(string orderBy, int pageIndex, int pageSize)
        {
            Expression<Func<Tag, bool>> expression = null;

            Func<IQueryable<Tag>, IIncludableQueryable<Tag, object>> include = null;

            var data = await GetDynamicAsync(expression, null, include, pageIndex, pageSize, true);


            return data;
        }
    }
}
