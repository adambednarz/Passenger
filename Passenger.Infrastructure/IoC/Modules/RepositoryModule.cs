using Autofac;
using Passenger.Core.Repositories;
using System.Reflection;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class UsersController : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(UsersController)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.IsAssignableTo<IRepository>())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
