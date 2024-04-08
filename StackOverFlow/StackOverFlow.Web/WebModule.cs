using Autofac;
using StackOverFlow.Web.Areas.User.Models;
using StackOverFlow.Web.Models;

namespace StackOverFlow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationModel>().AsSelf();

            builder.RegisterType<LoginModel>().AsSelf();


            builder.RegisterType<QuestionCreateModel>().AsSelf();

            builder.RegisterType<QuestionDetailsModel>().AsSelf();

<<<<<<< HEAD
            builder.RegisterType<QuestionListModel>().AsSelf();
=======
            builder.RegisterType<AnswerCreateModel>().AsSelf();
>>>>>>> 28187c25e05d8928b3b3e7b7921377adbf241454

            base.Load(builder);
        }
    }
}
