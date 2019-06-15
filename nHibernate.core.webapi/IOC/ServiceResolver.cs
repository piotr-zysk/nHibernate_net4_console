using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using nHibernate.core.webapi.Repositories;
using NHibernate.Cfg;
using System;
using System.Reflection;

namespace nHibernate.core.webapi.IOC
{
    public class ServiceResolver
    {
        private static WindsorContainer container;
        private static IServiceProvider serviceProvider;

        public ServiceResolver(IServiceCollection services)
        {
            container = new WindsorContainer();

            container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly())
                .BasedOn<IUserRepository>().WithService.FromInterface());

            
            container.Register(Component
                .For<NHibernate.ISessionFactory>()
                .Instance(new Configuration().Configure().BuildSessionFactory()));

            container.Register(Component
                .For<NHibernate.ISession>()
                .UsingFactoryMethod(kernel => kernel.Resolve<NHibernate.ISessionFactory>().OpenSession())
                .LifeStyle.Transient);
                       
            serviceProvider = WindsorRegistrationHelper.CreateServiceProvider(container, services);
        }

        public IServiceProvider GetServiceProvider()
        {
            return serviceProvider;
        }
    }
}
