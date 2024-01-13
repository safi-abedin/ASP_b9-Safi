using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        Task<Book> GetBookAsync(Guid id);
        Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(int pageIndex, int pageSize, string title, DateTime publishDateFrom, DateTime publishDateTo, string v);
    }
}
