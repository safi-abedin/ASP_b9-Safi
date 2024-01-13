using Library.Domain.Entities;
using Library.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        public BookRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(int pageIndex, int pageSize, string title, DateTime publishDateFrom, DateTime publishDateTo, string v)
        {
            Expression<Func<Book, bool>> expression = null;

            if(!string.IsNullOrEmpty(title))
            {
                expression = x=>x.Title.Contains(title) && x.PublishDate>=publishDateFrom && x.PublishDate<=publishDateTo;
            }

            return await GetDynamicAsync(expression, v, null, pageIndex, pageSize, true);
        }
    }
}
