using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Repositories
{
    public interface ITagRepository : IRepositoryBase<Tag,Guid>
    {
    }
}
