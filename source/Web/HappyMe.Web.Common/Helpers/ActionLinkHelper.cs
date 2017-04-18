namespace HappyMe.Web.Common.Helpers
{
    using System;
    using System.Collections.Generic;
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
            return helper.ActionLink("Към начало администрация", "Index", new { controller = "Dashboard", area = "Administration" }, new { @class = "btn btn-primary bottom-buffer" });
        }
    }
}
