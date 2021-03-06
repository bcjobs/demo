﻿using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using Infra.Events;
using Infra.Events.Dispatching;
using Infra.IoC;
using Infra.Mixins;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.Web
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());

            Types.Referenced.KindOf("Services")
                .Classes()
                .ForAll(t =>
                {
                    builder
                        .RegisterType(t)
                        .AsImplementedInterfaces()
                        .InstancePerRequest();
                });

            Types.Referenced.KindOf("Services.Singletons")
                .Classes()
                .ForAll(t =>
                {
                    builder
                        .RegisterType(t)
                        .AsImplementedInterfaces()
                        .SingleInstance();
                });

            Types.Referenced.KindOf("Services.Transient")
                .Classes()
                .ForAll(t =>
                {
                    builder
                        .RegisterType(t)
                        .AsImplementedInterfaces()
                        .InstancePerDependency();
                });

            Types.Referenced.With<MixinAttribute>()
                .ForAll(t => builder
                    .RegisterType(Mixin.Emit(t))
                    .As(t));

            Assemblies.Referenced
                .ForAll(a => builder.RegisterApiControllers(a));

            builder.RegisterType<ServiceProvider>()
                .AsImplementedInterfaces();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Event.Subscribe(new EventDispatcher(container.Resolve<IServiceProvider>()));
        }
    }

    class ServiceProvider : IServiceProvider
    {
        public ServiceProvider(IComponentContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Context != null);
            Context = context;
        }

        public IComponentContext Context { get; }

        public object GetService(Type serviceType)
        {
            return Context.Resolve(serviceType);
        }
    }
}
