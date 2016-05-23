using Te4Fest.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Te4Fest.Web
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;

    using Te4Fest.Common.Constants;
    using Te4Fest.Data;
    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Services.Common;
    using Te4Fest.Services.Common.Mapping;
    using Te4Fest.Services.Common.Mapping.Contracts;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>));
            kernel.Bind(typeof(IDeletableEntityRepository<>)).To(typeof(EfDeletableEntityRepository<>));
            kernel.Bind<DbContext>().To<Te4FestDbContext>().InRequestScope();
            kernel.Bind<IMappingService>().To<AutoMapperMappingService>();

            kernel.Bind(typeof(UserManager<>)).To(typeof(ApplicationUserManager));
            kernel.Bind(typeof(IUserStore<>)).To(typeof(UserStore<>));

            kernel.Bind(k => k
                .From(
                    AssemblyConstants.ServicesData,
                    AssemblyConstants.ServicesAdministration,
                    AssemblyConstants.ServicesCommon)
                .SelectAllClasses()
                .InheritedFrom<IService>()
                .BindDefaultInterface());
        }        
    }
}
