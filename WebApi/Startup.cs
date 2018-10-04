using System;
using System.Linq;
using DomainModel;
using Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

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

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));

            RegisterDependencies();

            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        private void RegisterDependencies()
        {
            _container.Register<IProductService, ProductService>();
            _container.Register<ILogger, ConsoleLogger>();
            _container.RegisterInstance<IEntityStore<Product>>(ProductStore.Current);

            _container.Register<IProductRepository, ProductRepository>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.FullName.StartsWith("Microsoft") &&
                            !a.FullName.StartsWith("System")).ToList();

            _container.Register(typeof(IRepository<>), assemblies);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            _container.RegisterMvcControllers(app);
            _container.AutoCrossWireAspNetComponents(app);

            app.UseMvc();
        }
    }
}
