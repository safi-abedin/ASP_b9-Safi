using Autofac;
using StackOverFlow.Web.Models;

namespace StackOverFlow.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegistrationModel>().AsSelf();

            base.Load(builder);
        }
    }
}
