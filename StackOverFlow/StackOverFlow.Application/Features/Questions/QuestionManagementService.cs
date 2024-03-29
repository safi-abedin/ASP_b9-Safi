using Microsoft.AspNetCore.Identity;
using StackOverFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StackOverFlow.Application.Features.Questions
{
    public class QuestionManagementService : IQuestionManagementService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

       
        public QuestionManagementService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateQuestionAsync(string title, string body, List<Tag> tags)
        {
            var question = new Question
            {
                title = title,
                Body = body,
                Tags = tags,
                //have to replace later with actual user Id
                CreatorUserId = Guid.NewGuid(),
                CreationDateTime = DateTime.Now
            };

            await _unitOfWork.QuestionRepository.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }
    }
}
