using Autofac;
using StackOverFlow.API.RequestHandlers;

namespace StackOverFlow.API
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionsRequestHandlers>().AsSelf();
            base.Load(builder);
        }
    }
}
