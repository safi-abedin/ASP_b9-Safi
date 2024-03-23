using Autofac;
using StackOverFlow.Domain.Entities;

namespace StackOverFlow.Web.Areas.User.Models
{
    public class QuestionCreateModel
    {
		private ILifetimeScope _scope;

        public string Title { get; set; }

        public string Details { get; set; }

        public string TriedApproach { get; set; }

        public ICollection<QuestionTag> Tags { get; set; }

        public QuestionCreateModel() { }


        internal void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
        }


    }
}
