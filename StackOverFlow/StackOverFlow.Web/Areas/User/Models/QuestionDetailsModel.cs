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


        public string CreatorEmail { get; set; }


        public DateTime CreationDateTime { get; set; }

        public List<Tag> Tags { get; set; }


        public string AnsweredByUserImageUrl;

        public string QuestionedByUserImageUrl;


        public List<Answer> Answers { get; set; }

        public int ViewCount { get; set; }


        public int VoteCount { get; set; }

        public int AnswerCount { get; set; }

        public string? ProfilePictureUrl { get; set; }



        public string? DisplayName { get; set; }


        public string AnswredByDisplayName { get; set; }

        public string AnswerdByUserImageUrl { get; set; }


        public int? AnswerdByUserReputation { get; set; }


        public int? Reputation { get; set; }


        public string ImageURL { get; set; }



        public Dictionary<string, AnswerdByUserModel> AnswerdByUserDetails { get; set; }


        //properties for Create



        public string TriedApproach { get; set; }



        private ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;


        public IPhotoService _photoService;


        private IMapper _mapper;


        private UserManager<ApplicationUser> _userManager { get; set; }


        public QuestionDetailsModel()
        {
            AnswerdByUserDetails = new Dictionary<string, AnswerdByUserModel>();
        }

        public QuestionDetailsModel(IQuestionManagementService questionManagementService, IMapper mapper,
            IPhotoService photoService,UserManager<ApplicationUser> userManager)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
            _photoService = photoService;
            _userManager = userManager;
            AnswerdByUserDetails = new Dictionary<string, AnswerdByUserModel>();
        }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
            _photoService = _scope.Resolve<IPhotoService>();
            _userManager = _scope.Resolve<UserManager<ApplicationUser>>();
        }

        internal async Task LoadAsync(Guid id)
        {
            var question = await _questionManagementService.GetQuestionAsync(id);

            if (question == null)
            {
                return;
            }

            await _questionManagementService.IncreaseView(id);

            Id = question.Id;
            title = question.title;
            Body = WebUtility.HtmlDecode(question.Body);

            Tags = question.Tags?.ToList() ?? new List<Tag>();

            if (question.Answers != null)
            {
                Answers = question.Answers.ToList();
                foreach (var answer in Answers)
                {
                    var email = answer.AnsweredByCreatorEmail;
                    var answerUser = await _userManager.FindByEmailAsync(email);


                    if (answerUser == null)
                    {

                        DisplayName = "Questioned User Did Not Have a Display Name";
                        Reputation = 0;
                        QuestionedByUserImageUrl = "~/AdminLTE/img//avatar3.png";
                    }
                    else
                    {
                        AnswredByDisplayName = !string.IsNullOrEmpty(answerUser.DisplayName) ? answerUser.DisplayName : "Answered By User Did Not Have a Display Name";
                        AnswerdByUserReputation = answerUser.Reputation == null ? 0 : answerUser.Reputation;
                        AnswerdByUserImageUrl = answerUser.ProfilePictureUrl != null ? await _photoService.GetPhotoAsync(answerUser.ProfilePictureUrl) : "~/AdminLTE/img//avatar3.png";
                    }

                    if (!AnswerdByUserDetails.ContainsKey(email))
                    {
                        AnswerdByUserDetails.Add(email, new AnswerdByUserModel
                        {
                            Email = email,
                            AnswerdByUserImageUrl = AnswerdByUserImageUrl,
                            AnswerdByUserReputation = AnswerdByUserReputation,
                            AnswredByDisplayName = AnswredByDisplayName
                        });
                    }
                }
            }

            CreationDateTime = question.CreationDateTime;
            CreatorUserId = question.CreatorUserId;
            CreatorEmail = question.CreatorEmail;

            var user = await _userManager.FindByEmailAsync(CreatorEmail);

            if (user == null)
            {
                DisplayName = "Questioned User Did Not Have a Display Name";
                Reputation = 0; 
                QuestionedByUserImageUrl = "~/AdminLTE/img//avatar3.png";
            }
            else
            {
                DisplayName = !string.IsNullOrEmpty(user.DisplayName) ? user.DisplayName : "Questioned User Did Not Have a Display Name";
                Reputation = user.Reputation;
                QuestionedByUserImageUrl = user.ProfilePictureUrl != null ? await _photoService.GetPhotoAsync(user.ProfilePictureUrl) : "~/AdminLTE/img//avatar3.png";
            }

            ViewCount = question.ViewCount;
            VoteCount = question.VoteCount;
            AnswerCount = question.AnswerCount;
        }


        internal async Task CreateAnswerAsync(Guid userId,string userEmail)
        {
           await  _questionManagementService.CreateAnswerAsync(Id,TriedApproach, userId,userEmail);
        }
        internal async Task<string> GetPhotoAsync(string? key)
        {
            return await _photoService.GetPhotoAsync(key);
        }
    }
}
