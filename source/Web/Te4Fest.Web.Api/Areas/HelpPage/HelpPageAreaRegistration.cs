namespace Te4Fest.Web.Api.Areas.HelpPage
{
    using System.Web.Http;
    using System.Web.Mvc;

    using Te4Fest.Web.Api.Areas.HelpPage.App_Start;

    public class HelpPageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HelpPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HelpPage_Default", 
                "Help/{action}/{apiId}", 
                new { controller = "Help", action = "Index", apiId = UrlParameter.Optional });

            HelpPageConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}