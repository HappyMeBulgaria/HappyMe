namespace HappyMe.Web.Areas.Administration
{
    using System.Web.Mvc;

    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "Administration";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Administration_children_statistics",
                "Administration/ChildrenStatistics/Index/{username}",
                new { controller = "ChildrenStatistics", action = "Index", username = UrlParameter.Optional },
                namespaces: new[] { "HappyMe.Web.Areas.Administration.Controllers" });

            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HappyMe.Web.Areas.Administration.Controllers" });
        }
    }
}