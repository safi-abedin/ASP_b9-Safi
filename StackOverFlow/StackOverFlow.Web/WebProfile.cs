using AutoMapper;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Web.Areas.User.Models;

namespace StackOverFlow.Web
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<Question, QuestionDetailsModel>().ReverseMap();
        }
    }
}
