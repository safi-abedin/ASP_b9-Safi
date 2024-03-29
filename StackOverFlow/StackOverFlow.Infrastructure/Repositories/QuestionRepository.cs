using Microsoft.EntityFrameworkCore;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Infrastructure.Repositories
{
    public class QuestionRepository : Repository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }
    }
}
