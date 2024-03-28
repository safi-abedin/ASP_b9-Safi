using Autofac;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionCreateModel
    {
        private ILifetimeScope _scope;

        public string Title { get; set; }

        public string Details { get; set; }

        public string TriedApproach { get; set; }

        public List<string> Tags { get; set; }

        //Display property
        public List<SelectListItem> MultiTags { get; set; }

        public QuestionCreateModel() { }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }

    }
}
