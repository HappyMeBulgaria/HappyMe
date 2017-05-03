namespace HappyMe.Web
{
    using System;
    using System.Data.Entity;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using HappyMe.Common.Mapping;
    using HappyMe.Data;
    using HappyMe.Data.Migrations;

    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());
        }

        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HappyMeDbContext, DefaultMigrationConfiguration>());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.RegisterMappings(Assembly.GetExecutingAssembly());

            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("x-frame-options", "SAMEORIGIN");
        }
    }
}
