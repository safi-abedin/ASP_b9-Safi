using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Management
{
    public interface IBookManagementService
    {
        Task CreateBookAsync(string title, DateTime publishDate, string authorName);
        Task DeleteBookAsync(Guid id);
        Task<Book> GetBookAsync(Guid id);
        Task<(IList<Book> records,int total,int totalDisplay)> GetTableDataAsync(int pageIndex, int pageSize, string title, DateTime publishDateFrom, DateTime publishDateTo, string v);
        Task UpdateBookAsync(Guid id, string title, DateTime publishDate, string authorName);
    }
}
