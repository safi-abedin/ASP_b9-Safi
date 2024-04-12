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

        public async Task CreateAnswerAsync(Guid questionId, string answerBody,Guid UserId)
        {
            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerTime = DateTime.Now,
                //have to change later
                AnsweredByUserId = Guid.NewGuid(),
                Body = answerBody,
            };

            var question = await _unitOfWork.QuestionRepository.GetAsync(questionId);

            if(question != null)
            {

                question.Answers.Add(answer);

            }

            await _unitOfWork.SaveAsync(); 
        }

        public async Task CreateQuestionAsync(string title, string Details,string TriedApproach, List<string> tags,Guid UserId)
        {
            var allTags = _unitOfWork.TagRepository.GetAll();
            var selectedTags = new List<Tag>();

            foreach (var tag in tags)
            {
                selectedTags.Add(new Tag { Id = Guid.NewGuid(), Name = tag });
            }

            var selectedAndInAllTags = new List<Tag>();

            foreach (var tag in selectedTags)
            {
                if (allTags.Any(t => t.Name == tag.Name))
                {
                    selectedAndInAllTags.Add(allTags.FirstOrDefault(t => t.Name == tag.Name));
                }
            }


            var question = new Question
            {
                title = title,
                Body = Details+TriedApproach,
                Details = Details,
                TriedApproach = TriedApproach,
                Tags = selectedAndInAllTags,
                CreatorUserId = UserId,
                CreationDateTime = DateTime.Now
            };

            await _unitOfWork.QuestionRepository.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(Guid id,string title, string Details,string TriedApproach, List<string> tags)
        {
            var allTags = _unitOfWork.TagRepository.GetAll();
            var selectedTags = new List<Tag>();

            foreach (var tag in tags)
            {
                selectedTags.Add(new Tag { Id = Guid.NewGuid(), Name = tag });
            }

            var selectedAndInAllTags = new List<Tag>();

            foreach (var tag in selectedTags)
            {
                if (allTags.Any(t => t.Name == tag.Name))
                {
                    selectedAndInAllTags.Add(allTags.FirstOrDefault(t => t.Name == tag.Name));
                }
            }

            var question = await _unitOfWork.QuestionRepository.GetAsync(id);

            if(question is  not null)
            {
                question.title = title;
                question.Body = Details+TriedApproach;
                question.Details = Details;
                question.TriedApproach = TriedApproach;
                question.Tags = selectedAndInAllTags;
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _unitOfWork.TagRepository.GetAllAsync();
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAsync(int pageIndex, int pageSize, string orderBy)
        {
            return await _unitOfWork.QuestionRepository.GetTableDataAsync(orderBy, pageIndex, pageSize);
        }

        public async Task<Question> GetQuestionAsync(Guid id)
        {
            return await _unitOfWork.QuestionRepository.GetAsync(id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            var data = await _unitOfWork.QuestionRepository.GetAllQuestionsAsync();
            return data;
        }

    }
}
