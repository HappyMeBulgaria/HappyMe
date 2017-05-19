namespace HappyMe.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using HappyMe.Common.Mapping;
    using HappyMe.Data;
    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Common;
    using HappyMe.Services.Common.Mapping;
    using HappyMe.Services.Common.Mapping.Contracts;
    using HappyMe.Services.Data.Contracts;
    using HappyMe.Web.Config;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<HappyMeDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("HappyMe.Web")));

            services.AddIdentity<User, IdentityRole>(x =>
                {
                    x.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<HappyMeDbContext>()
                .AddDefaultTokenProviders();

            services.AddSession();
            services.AddMvc();

            AutoMapperConfig.RegisterMappings(typeof(Startup).GetTypeInfo().Assembly);

            // Add application services.
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped<DbContext, HappyMeDbContext>();
            services.AddScoped<DbContext, HappyMeDbContext>();
            services.AddScoped<IMappingService, AutoMapperMappingService>();
            services.Add(new ServiceDescriptor(typeof(IMapper), AutoMapperConfig.MapperConfiguration?.CreateMapper()));
            services.AddScoped<RoleManager<IdentityRole<string>>, RoleManager<IdentityRole<string>>>();
            //// services.AddTransient<ISmsSender, AuthMessageSender>();

            // TODOD: Move to constants or somewhere else
            var serviceAssemblies = new[]
            {
                typeof(Startup).GetTypeInfo().Assembly,
                typeof(IService).GetTypeInfo().Assembly,
                typeof(IAdministrationService<>).GetTypeInfo().Assembly,
                typeof(IUsersDataService).GetTypeInfo().Assembly
            };

            this.RegesterServiceFromType(services, serviceAssemblies, typeof(IService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            //// Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseSession();
            app.UseMvc(RouteConfig.RegisterRoutes);
        }

        private void RegesterServiceFromType(
            IServiceCollection services,
            IEnumerable<Assembly> serviceAssemblies,
            Type typeToRegister)
        {
            var nonGenericTypeServiceRegistrationsInfo = serviceAssemblies
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => typeToRegister.IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsGenericTypeDefinition)
                .Select(t => new
                {
                    ConcreteType = t,
                    ServiceTypes = t
                        .GetInterfaces()
                        .Where(i =>
                            i.GetTypeInfo().IsPublic &&
                            i != typeToRegister &&
                            !i.GenericTypeArguments.Any())
                })
                .ToList();

            foreach (var serviceType in nonGenericTypeServiceRegistrationsInfo)
            {
                foreach (var type in serviceType.ServiceTypes)
                {
                    services.Add(new ServiceDescriptor(type, serviceType.ConcreteType, ServiceLifetime.Scoped));
                }
            }
        }
    }
}
