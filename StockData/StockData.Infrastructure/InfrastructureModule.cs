﻿using Autofac;
using Exam1.Application;
using Exam1.Application.Features.Money;
using Exam1.Domain.Repositories;
using Exam1.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockData.Infrastructure
{
    public class InfrastructureModule:Module
    {


        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public InfrastructureModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationAssembly)
               .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString",_connectionString)
                .WithParameter("migrationAssembly",_migrationAssembly)
                .InstancePerLifetimeScope();


            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ExpenseRepository>().As<IExpenseRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
