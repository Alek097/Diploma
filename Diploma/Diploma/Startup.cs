using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Diploma.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Diploma.Data;
using Diploma.Repositories;
using Diploma.Core.ConfigureModels;
using Diploma.Repositories.Interfaces;
using Diploma.BusinessLogic.Interfaces;
using Diploma.BusinessLogic;

namespace Diploma
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            services.AddDbContext<ApplicationContext>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationContext, Guid>()
                .AddDefaultTokenProviders();

            services.AddTransient<IContext, ApplicationContext>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IBanRepository, BanRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICharacteristicRepository, CharacteristicRepository>();
            services.AddTransient<ICharacteristicsGroupRepository, CharacteristicsGroupRepository>();
            services.AddTransient<IOAuthStateRepository, OAuthStateRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IEditEmailConfirmMessageRepository, EditEmailConfirmMessageRepository>();

            services.AddTransient<IAuthorizeBusinessLogic, AuthorizeBusinessLogic>();
            services.AddTransient<IProfileBussinessLogic, ProfileBussinessLogic>();

            services.Configure<List<OAuth>>(this.Configuration.GetSection("OAuth"));
            services.Configure<App>(this.Configuration.GetSection("App"));
            services.Configure<Email>(this.Configuration.GetSection("Email"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();

            app.AddNLogWeb();
            app.UseIdentity();
            app.UseMvc();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Bundles")),
                RequestPath = "/Bundles"
            });

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Assets")),
                RequestPath = "/img"
            });
        }
    }
}
