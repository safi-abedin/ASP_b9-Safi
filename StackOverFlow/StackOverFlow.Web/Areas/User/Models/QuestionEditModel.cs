using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionEditModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string Details { get; set; }

        [Required]
        public string TriedApproach { get; set; }


        [Required]
        public List<string> Tags { get; set; }

        //Display property
        public List<SelectListItem>? MultiTags { get; set; }


        public ILifetimeScope _scope;

        public IQuestionManagementService _questionManagementService;

        private IMapper _mapper;


        public QuestionEditModel()
        {

        }

        public QuestionEditModel(IQuestionManagementService questionManagementService,IMapper mapper)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
        }

        internal void ResolveAsync(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
            _mapper = _scope.Resolve<IMapper>();
        }

        internal async Task<IEnumerable<Tag>> GetAvailableTags()
        {
            return await _questionManagementService.GetAllTags();
        }

        internal async Task LoadAsync(Guid id)
        {
            var question = await _questionManagementService.GetQuestionAsync(id);
            _mapper.Map(question, this);
        }

        internal async Task EditAsync()
        {
            await _questionManagementService.EditAsync(Id,title,Details+TriedApproach,Tags);
        }
    }
}
