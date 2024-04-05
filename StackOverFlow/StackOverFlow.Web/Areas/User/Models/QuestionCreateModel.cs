using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure.Membership;
using System.ComponentModel.DataAnnotations;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionCreateModel
    {

        public ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string TriedApproach { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public List<string> Tags { get; set; }

        //Display property
        public List<SelectListItem>? MultiTags { get; set; }


        public QuestionCreateModel()
        {

        }

        public QuestionCreateModel(IQuestionManagementService questionManagementService)
        {
            _questionManagementService = questionManagementService;
        }

        internal void ResolveAsync(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
        }

        internal async Task CreateAsync()
        {
            var body = Details + TriedApproach;
            
            await _questionManagementService.CreateQuestionAsync(Title, body, Tags,UserId);
        }


    }
}
