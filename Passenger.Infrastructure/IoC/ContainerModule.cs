using Autofac;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;

namespace Passenger.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<UsersController>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                   .SingleInstance();
        }
    }
}
