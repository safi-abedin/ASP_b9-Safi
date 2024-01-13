using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Features.Management
{
    public class BookManagementService : IBookManagementService
    {
        private  IApplicationUnitOfWork _unitOfWork;

        public BookManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateBookAsync(string title, DateTime publishDate, string authorName)
        {
            var book = new Book
            {
                Title = title,
                PublishDate = publishDate,
                AuthorName = authorName
            };

            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteBookAsync(Guid id)
        {
            await _unitOfWork.BookRepository.RemoveAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _unitOfWork.BookRepository.GetBookAsync(id);
        }

        public async Task<(IList<Book> records, int total, int totalDisplay)> GetTableDataAsync(int pageIndex, int pageSize, string title, DateTime publishDateFrom, DateTime publishDateTo, string v)
        {
            return await _unitOfWork.BookRepository.GetTableDataAsync(pageIndex, pageSize, title, publishDateFrom, publishDateTo, v);
        }

        public async Task UpdateBookAsync(Guid id, string title, DateTime publishDate, string authorName)
        {
            var book = await _unitOfWork.BookRepository.GetBookAsync(id);

            if(book is not null)
            {
                book.Title = title;
                book.AuthorName = authorName;
                book.PublishDate = publishDate;
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
