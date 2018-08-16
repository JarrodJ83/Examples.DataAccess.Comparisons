using DomainModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Queries;
using Repositories;
using Repositories.Core;
using Requests;
using Services;
using Services.Core;
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

            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            RegisterServicesAndRepos(_container);
            RegisterCQRSDependencies(_container);

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(_container));
            
            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
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

        private void RegisterServicesAndRepos(Container container)
        {
            //    container.Register(typeof(IRepository<>), new[] { typeof(ProductRespository).Assembly }, Lifestyle.Scoped);
            //    container.Register<IProductRepository, ProductRespository>(Lifestyle.Scoped);
            //    container.Register<IProductService, ProductService>(Lifestyle.Scoped);
        }

        private void RegisterCQRSDependencies(Container container)
        {
            container.Register(typeof(IRequestHandler<>), new[] { typeof(RequestHandlers.GetAllProductsPaged).Assembly }, Lifestyle.Scoped);
            container.Register(typeof(IRequestHandler<,>), new[] { typeof(RequestHandlers.GetAllProductsPaged).Assembly }, Lifestyle.Scoped);
            //container.Register(typeof(ICommandHandler<>), assemblies, Lifestyle.Scoped);
            container.Register(typeof(IQueryHandler<,>), new[] { typeof(QueryHandlers.AllProductsPaged).Assembly }, Lifestyle.Scoped);
            
            container.Register<IEntityStore<Product>>(() => new ProductStore(100), Lifestyle.Singleton);

        }
    }
}
