using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure
{
    public interface IApplicationDbContext
    {
       DbSet<Book> Books { get; set; }
    }
}