using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;
using StackOverFlow.Domain.Entities;
using StackOverFlow.Infrastructure.Membership;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionCreateModel
    {

        public string Title { get; set; }

        public string Details { get; set; }

        public string TriedApproach { get; set; }

        public Guid? UserId { get; set; }

        public List<string> Tags { get; set; }

        //Display property
        public List<SelectListItem>? MultiTags { get; set; }


    }
}
