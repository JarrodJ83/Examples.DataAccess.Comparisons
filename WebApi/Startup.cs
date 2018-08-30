using DomainModel;
using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queries;
using RequestHandlers;
using Requests;
using Serilog;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using ILogger = Logging.ILogger;

namespace WebApi
{
    public class Startup
    {
        private Container _container;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container = new Container();
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterCQRSDependencies(_container);
            
            _container.Register<ILogger, SerilogLogger>(Lifestyle.Scoped);
            _container.Register<Serilog.ILogger>(CreateSerilog, Lifestyle.Scoped);

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            
            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        Serilog.ILogger CreateSerilog()
        {
            return new LoggerConfiguration()
                .WriteTo.Console()
                .MinimumLevel.Verbose()
                .CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);
            _container.AutoCrossWireAspNetComponents(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
        
        private void RegisterCQRSDependencies(Container container)
        {
            container.Register(typeof(IRequestHandler<>), new[] { typeof(RequestHandlers.GetAllProductsPaged).Assembly }, Lifestyle.Scoped);
            container.Register(typeof(IRequestHandler<,>), new[] { typeof(RequestHandlers.GetAllProductsPaged).Assembly }, Lifestyle.Scoped);
            
            container.RegisterDecorator(typeof(IRequestHandler<,>), typeof(LoggedRequestHandler<,>));
            
            //container.Register(typeof(ICommandHandler<>), assemblies, Lifestyle.Scoped);
            container.Register(typeof(IQueryHandler<,>), new[] { typeof(QueryHandlers.AllProductsPaged).Assembly }, Lifestyle.Scoped);
            container.RegisterInstance<IProductStore>(ProductStore.Current);
        }
    }
}
