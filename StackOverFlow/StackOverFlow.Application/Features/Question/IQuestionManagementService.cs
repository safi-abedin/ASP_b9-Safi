using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application.Features.Question
{
    public interface IQuestionManagementService
    {
        Task CreateQuestionAsync(string title, string body, List<string> tags);
    }
}
