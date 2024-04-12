using StackOverFlow.Domain;
using StackOverFlow.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        IQuestionRepository QuestionRepository { get; }

        ITagRepository TagRepository { get;}


        IAnswerRepository AnswerRepository { get; }

    }
}
