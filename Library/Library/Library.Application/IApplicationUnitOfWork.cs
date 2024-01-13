using Library.Domain;
using Library.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IBookRepository BookRepository { get; }
    }
}
