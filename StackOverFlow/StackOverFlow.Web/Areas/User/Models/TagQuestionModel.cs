using Autofac;
using AutoMapper;
using StackOverFlow.Application.Features.Photos;
using StackOverFlow.Application.Features.Questions;
using StackOverFlow.Domain.Entities;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class TagQuestionModel
    {

        public string tagName { get; set; }
        public int TotalCount { get; set; }


        public string DisplayName { get; set; }

        public string ImageURL { get; set; }


        public IList<Question> Questions { get; set; }

        private ILifetimeScope _scope;

        private IQuestionManagementService _questionManagementService;

        private IMapper _mapper;

        private IPhotoService _photoService;




        public TagQuestionModel()
        {
        }

        public TagQuestionModel(IQuestionManagementService questionManagementService,IMapper mapper,IPhotoService photoService)
        {
            _questionManagementService = questionManagementService;
            _mapper = mapper;
            Questions = new List<Question>();
            _photoService = photoService;
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _questionManagementService = _scope.Resolve<IQuestionManagementService>();
            _mapper = _scope.Resolve<IMapper>();
            _photoService = _scope.Resolve<IPhotoService>();
        }


        internal async Task GetTagedQuestonsAsync(Guid id)
        {
            var questions = await _questionManagementService.GetTagedQuestionsAsync(id);

            var tag = await _questionManagementService.GetTag(id);

            if(tag is not null)
            {
                tagName = tag.Name;
            }

            if(questions is not null)
            {
                Questions = questions;
                TotalCount = questions.Count;
            }
            
        }

        internal async Task<string> GetPhotoAsync(string? key)
        {
            return await _photoService.GetPhotoAsync(key);
        }
    }
}
