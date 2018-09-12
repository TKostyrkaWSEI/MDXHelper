using Autofac;
using MDXHelperData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MDXHelperApp
{
    public class IocConfig
    {
        private readonly IContainer container;

        public IocConfig()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AppDbContext>().InstancePerLifetimeScope();
            //builder.RegisterType<Processor>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Loader>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<DBCommunicator>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Divider>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<Extractor>().AsImplementedInterfaces().InstancePerLifetimeScope();

            container = builder.Build();
        }

        public IContainer GetContainer()
        {
            return container;
        }
    }
}
