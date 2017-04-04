namespace HappyMe.Web.Common.Helpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    public static class ActionLinkHelper
    {
        public static MvcHtmlString ActiveActionLinkHelper(this HtmlHelper helper, string linkText, string actionName, string controllerName, RouteValueDictionary routeValues, RouteValueDictionary htmlAttributes)
        {
            var currentControllerName = helper.ViewContext.Controller.GetType().Name;
            
            if (currentControllerName.Equals(controllerName + "Controller", StringComparison.OrdinalIgnoreCase))
            {
                htmlAttributes["class"] += " active";
            }

            return helper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString DashboardLink(this HtmlHelper helper)
        {
            return helper.ActionLink("Dashboard", "Index", new { controller = "Начална страница", area = "Administration" }, new { @class = "btn btn-primary bottom-buffer" });
        }
    }
}
