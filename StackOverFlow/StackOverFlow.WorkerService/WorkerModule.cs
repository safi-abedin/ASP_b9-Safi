using Autofac;
using StackOverFlow.WorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverFlow.WorkerService
{
    public class WorkerModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WorkerModel>().AsSelf();
        }

     }
}
