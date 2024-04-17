using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StackOverFlow.Application.Features.Photos;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionDetailsModel
    {
        //properties to show details
        public Guid Id { get; set; }

        public string title { get; set; }

        public string Body { get; set; }


        public Guid CreatorUserId { get; set; }


        public DateTime CreationDateTime { get; set; }

        public List<Tag> Tags { get; set; }


        public List<Answer> Answers { get; set; }

        public int ViewCount { get; set; }


        public int VoteCount { get; set; }

        public int AnswerCount { get; set; }

        public string? ProfilePictureUrl {  get; set; }



        public string? DisplayName { get; set; }


        public int? Reputation {  get; set; }


        public string ImageURL { get; set; }


        //properties for Create



        public string TriedApproach { get; set; }



        private ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;


        public IPhotoService _photoService;


        private IMapper _mapper;


        public QuestionDetailsModel()
        {

        }

        public QuestionDetailsModel(IQuestionManagementService questionManagementService, IMapper mapper,IPhotoService photoService)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
            _photoService = photoService;
        }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
            _photoService = _scope.Resolve<IPhotoService>();
        }

        internal async Task LoadAsync(Guid id)
        {
            var question = await _questionManagementService.GetQuestionAsync(id);

            await _questionManagementService.IncreaseView(id);


            if (question != null)
            {
                Id = question.Id;
                title = question.title;
                Body = WebUtility.HtmlDecode(question.Body);

                Tags = question.Tags.ToList();


                if(question.Answers != null)
                {
                    Answers = question.Answers.ToList();
                }
                else
                {
                    Answers = new List<Answer>();
                }
                
                CreationDateTime = question.CreationDateTime;
                CreatorUserId = question.CreatorUserId;
                ViewCount = question.ViewCount;
                VoteCount = question.VoteCount;
                AnswerCount = question.AnswerCount;
            }
        }

        internal async Task CreateAnswerAsync(Guid userId)
        {
           await  _questionManagementService.CreateAnswerAsync(Id,TriedApproach, userId);
        }
        internal async Task<string> GetPhotoAsync(string? key)
        {
            return await _photoService.GetPhotoAsync(key);
        }
    }
}
