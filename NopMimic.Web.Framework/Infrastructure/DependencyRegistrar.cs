using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using NopMimic.Core;
using NopMimic.Core.Data;
using NopMimic.Core.Infrastructure;
using NopMimic.Core.Infrastructure.DependencyManagement;
using NopMimic.Data;
using NopMimic.Services;

namespace NopMimic.Web.Framework.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //help
            builder.RegisterType<CommonHelper>().SingleInstance();

            //data layer
            builder.Register(context => new NopObjectContext(context.Resolve<DbContextOptions<NopObjectContext>>()))
                .As<IDbContext>().InstancePerLifetimeScope();

            //repositories
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //services
            builder.RegisterType<StudentService>().As<IStudentService>().InstancePerLifetimeScope();
        }

        public int Order { get; }
    }
}
