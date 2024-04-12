using AutoMapper;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Web.Areas.User.Models;

namespace StackOverFlow.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<QuestionDetailsModel,Question>().ReverseMap();

            CreateMap<QuestionEditModel, Question>().ReverseMap();
        }
    }
}
