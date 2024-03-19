using Autofac;
using StackOverFlow.Web.Models;

namespace StackOverFlow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationModel>().AsSelf();

            builder.RegisterType<LoginModel>().AsSelf();

            base.Load(builder);
        }
    }
}
