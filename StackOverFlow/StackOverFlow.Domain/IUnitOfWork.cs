using System;
using System.Collections.Generic;
using System.Text;

namespace StackOverFlow.Domain
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        void Save();
        Task SaveAsync();
    }
}
