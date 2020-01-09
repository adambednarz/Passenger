using Autofac;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Handlers.Users;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Passenger.Infrastructure.IoC.Modules
{
    public class CommandModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Automatyczne prześwietla katalogi z CommandHandlerami i dopasowuje implementację z odpowiednim
            var assembly = typeof(CommandModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(ICommandHandler<>))
                .InstancePerLifetimeScope();
            //----------------------------------------------
            //powyższa metoda zrobi za nas dokładnie to (dla wszystkich CommandHandlerów
            builder.RegisterType<CreateUserHandler>()
             .As<ICommandHandler<CreateUser>>()
             .InstancePerLifetimeScope();

            builder.RegisterType<CommandDispatcher>()
                .As<ICommandDispatcher>()
                .InstancePerLifetimeScope();
        }
    }
}
