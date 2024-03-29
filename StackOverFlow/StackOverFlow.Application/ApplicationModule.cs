using Autofac;
using StackOverFlow.Application.Features.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionManagementService>().As<IQuestionManagementService>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
