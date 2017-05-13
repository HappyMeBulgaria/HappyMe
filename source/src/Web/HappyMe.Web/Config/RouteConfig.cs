namespace HappyMe.Web.Config
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(IRouteBuilder routes)
        {
            //routes.MapRoute(
            //    name: "modulesAdministration",
            //    template: "administration/modules",
            //    defaults: new { controller = "Modules", action = "Index", area = "Administration" });

            //routes.MapRoute(
            //    name: "modules",
            //    template: "{modules}",
            //    defaults: new { controller = "Modules", action = "Index", area = string.Empty },
            //    constraints: new { area = string.Empty });

            routes.MapRoute(
                name: "areaRoute",
                template: "{area:exists}/{controller=Home}/{action=Index}");

            routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}",
                    defaults: null,
                    constraints: null);
        }
    }
}
