using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Domain.Repositories
{
    public interface IAnswerRepository : IRepositoryBase<Answer, Guid>
    {
        Task<Answer> GetAsync(Guid answerId);
        Task<IList<Answer>> GetVoteAsync(Guid answerId, Guid userId);
    }
}
