using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mappers;
using Passenger.Infrastructure.Repositories;
using Passenger.Infrastructure.Services;

namespace Passenger.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IHostingEnvironment env /*IConfiguration configuration, IContainer appContainer*/)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            //ApplicationContainer = appContainer;
        }

        //This method gets called by the runtime.Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // \/ zostały dla nich stworzone marker interfejsy a następnie moduły,
            //które dodane zostały do kontynera w klasie Container module 
            //services.AddScoped<IUserRepository, InMemoryUserRepository>();
            //services.AddScoped<IUserService, UserService>();

            // \/ wydelegowane do ContainerModule
            //services.AddSingleton(AutoMapperConfig.Initialize());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(x => x.SerializerSettings.Formatting = Formatting.Indented);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //Container = builder.Build();
            // \/ wydelegowanie do Containermodule
            //builder.RegisterModule<CommandModule>();
            //builder.RegisterModule(new SettingsModule(Configuration));
            builder.RegisterModule(new ContainerModule(Configuration));
            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseMvc();
            applicationLifetime.ApplicationStopped.Register(() => Container.Dispose());
        }
    }
}
