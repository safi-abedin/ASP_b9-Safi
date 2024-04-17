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

        public async Task ChangeAnswerVoteAsync(Guid userId, Guid answerId, bool isUpVote,Guid questionId)
        {
            var answer = await _unitOfWork.AnswerRepository.GetAsync(answerId);

            if (answer == null)
            {
                throw new InvalidOperationException("Answer not found.");
            }

            var answerFiltered = await _unitOfWork.AnswerRepository.GetVoteAsync(answerId, userId);

            var existingVote = answerFiltered.FirstOrDefault().AnswerVotes;

            if (existingVote == null || existingVote.Count == 0)
            {
                var newVote = new AnswerVotes
                {
                    AnswerId = answerId,
                    VotedBYId = userId,
                    VoterEmail = "", 
                    Up = isUpVote,
                    Down = !isUpVote,
                    QuestionId = questionId,
                    Question = await _unitOfWork.QuestionRepository.GetAsync(questionId)
            };

                answer.AnswerVotes.Add(newVote);

                answer.VoteCount += isUpVote ? 1 : -1;

                await _unitOfWork.SaveAsync();
            }
            else
            {
                var vote = existingVote.FirstOrDefault();

                if ((isUpVote && vote.Up) || (!isUpVote && vote.Down))
                {
                    return;
                }

                vote.Up = isUpVote;
                vote.Down = !isUpVote;

                answer.VoteCount += isUpVote ? +2 : -2;

                await _unitOfWork.SaveAsync();
            }
        }


        public async Task GiveAnswerDownVoteAsync(Guid questionId, Guid userId, Guid answerId)
        {
            await ChangeAnswerVoteAsync(userId, answerId, false,questionId);
        }

        public async Task GiveAnswerUpVoteAsync(Guid questionId, Guid userId, Guid answerId)
        {
            await ChangeAnswerVoteAsync(userId, answerId, true,questionId);
        }


        public async Task<bool> CheckVote(Guid questionId, Guid userId)
        {
            await GiveVote(questionId, userId, true);
            return true;
        }

        public async Task GiveDownVote(Guid questionId, Guid userId)
        {
            await GiveVote(questionId, userId, false);
        }

        private async Task GiveVote(Guid questionId, Guid userId, bool isUpVote)
        {
            var result = await _unitOfWork.QuestionRepository.GetVoteAsync(questionId, userId);

            if (result == null || result.Count == 0)
            {
                var question = await _unitOfWork.QuestionRepository.GetAsync(questionId);

                if (question == null)
                {
                    throw new InvalidOperationException("Question not found.");
                }

                var vote = new QuestionVotes
                {
                    QuestionId = questionId,
                    VotedBYId = userId,
                    Up = isUpVote,
                    Down = !isUpVote,
                    VoterEmail = "", 
                    Question = question
                };

                question.VoteCount += isUpVote ? 1 : -1;

                question.Votes.Add(vote);
            }
            else
            {
                var existingVote = result.FirstOrDefault().Votes.FirstOrDefault();

                if (existingVote.Up && !isUpVote)
                {
                    existingVote.Up = false;
                    existingVote.Down = true;

                    existingVote.Question.VoteCount -= 2;
                }
                else if (existingVote.Down && isUpVote)
                {
                    existingVote.Up = true;
                    existingVote.Down = false;

                    existingVote.Question.VoteCount += 2;
                }
            }

            await _unitOfWork.SaveAsync();
        }
    

        public async Task CreateAnswerAsync(Guid questionId, string answerBody,Guid UserId,string userEmail)
        {
            var answer = new Answer
            {
                QuestionId = questionId,
                AnswerTime = DateTime.Now,
                AnsweredByUserId = UserId,
                AnsweredByCreatorEmail=userEmail,
                Body = answerBody,
            };

            var question = await _unitOfWork.QuestionRepository.GetAsync(questionId);


            if(question != null)
            {
                question.AnswerCount++;
                question.Answers.Add(answer);

            }

            await _unitOfWork.SaveAsync(); 
        }

        public async Task CreateQuestionAsync(string title, string Details,string TriedApproach, List<string> tags,Guid UserId,string UserEmail)
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
                CreatorEmail = UserEmail,
                CreationDateTime = DateTime.Now
            };

            await _unitOfWork.QuestionRepository.AddAsync(question);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            await _unitOfWork.QuestionRepository.RemoveAsync(id);
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

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAskedAsync(int pageIndex, int pageSize, Guid UserId)
        {
            return await _unitOfWork.QuestionRepository.GetQuestionsByUser(pageIndex,pageSize, UserId);
        }

        public async Task<(IList<Question> records, int total, int totalDisplay)> GetPagedQuestionsAsync(int pageIndex, int pageSize, string orderBy)
        {
            return await _unitOfWork.QuestionRepository.GetTableDataAsync(orderBy, pageIndex, pageSize);
        }

        public async Task<(IList<Tag> records, int total, int totalDisplay)> GetPagedTagsAsync(int pageIndex, int pageSize, string orderBy)
        {
            return await _unitOfWork.TagRepository.GetTableTagsAsync(orderBy, pageIndex, pageSize);
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

        public async Task<Tag> GetTag(Guid id)
        {
            return await _unitOfWork.TagRepository.GetByIdAsync(id);
           
        }

        public Task<IList<Question>> GetTagedQuestionsAsync(Guid id)
        {
            return _unitOfWork.QuestionRepository.GetTagedQuestions(id);
        }

        

        public async Task IncreaseView(Guid id)
        {
            var question = await _unitOfWork.QuestionRepository.GetAsync(id);

            if (question is not null)
            {
                question.ViewCount = question.ViewCount + 1;
            }

            await _unitOfWork.SaveAsync();
        }

    
    }
}
