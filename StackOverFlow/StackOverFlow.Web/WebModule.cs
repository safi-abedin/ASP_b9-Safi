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


            builder.RegisterType<QuestionListModel>().AsSelf();

            builder.RegisterType<TagListModel>().AsSelf();


            builder.RegisterType<AnswerCreateModel>().AsSelf();

            builder.RegisterType<QuestionEditModel>().AsSelf();

            builder.RegisterType<QuestionAskedListModel>().AsSelf();

            builder.RegisterType<ProfileEditModel>().AsSelf();

            builder.RegisterType<ProfileViewModel>().AsSelf();


            base.Load(builder);
        }
    }
}
