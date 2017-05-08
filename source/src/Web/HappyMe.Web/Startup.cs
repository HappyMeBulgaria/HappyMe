using System.Linq;
using System.Reflection;
using HappyMe.Data.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using HappyMe.Web.Services;

namespace HappyMe.Web
{
    using HappyMe.Data;

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

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<HappyMeDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            var serviceAssemblies = new[]
            {
                typeof(Startup).GetTypeInfo().Assembly
            };

            var nonGenericTypeServiceRegistrationsInfo = serviceAssemblies
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => typeof(IService).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsGenericTypeDefinition)
                .Select(t => new
                {
                    ConcreteType = t,
                    ServiceTypes = t
                        .GetInterfaces()
                        .Where(i =>
                            i.GetTypeInfo().IsPublic &&
                            i != typeof(IService) &&
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

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
