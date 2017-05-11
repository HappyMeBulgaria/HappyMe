namespace HappyMe.Web.Controllers.Base
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using HappyMe.Common.Constants;
    using HappyMe.Data.Models;
    using HappyMe.Web.Common;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        protected UserManager<User> UserManager { get; set; }


        protected internal RedirectToActionResult RedirectToAction<TController>(Expression<Action<TController>> expression)
            where TController : Controller
        {
            var method = expression.Body as MethodCallExpression;
            if (method == null)
            {
                throw new ArgumentException("Expected method call");
            }

            return this.RedirectToAction(method.Method.Name);
        }

        protected Task<User> GetCurrentUserAsync() => this.UserManager.GetUserAsync(this.HttpContext.User);

        protected async Task<string> GetUserIdAsync() => (await this.GetCurrentUserAsync())?.Id;


        ////protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        ////{
        ////    // Work with data before BeginExecute to prevent "NotSupportedException: A second operation started on this context before a previous asynchronous operation completed."

        ////    // Calling BeginExecute before PrepareSystemMessages for the TempData to has values
        ////    var result = base.BeginExecute(requestContext, callback, state);

        ////    var systemMessages = this.PrepareSystemMessages();
        ////    this.ViewBag.SystemMessages = systemMessages;

        ////    return result;
        ////}

        ////protected override void OnException(ExceptionContext filterContext)
        ////{
        ////    if (filterContext.ExceptionHandled)
        ////    {
        ////        return;
        ////    }

        ////    if (this.Request.IsAjaxRequest())
        ////    {
        ////        var exception = filterContext.Exception as HttpException;

        ////        if (exception != null)
        ////        {
        ////            this.Response.StatusCode = exception.GetHttpCode();
        ////            this.Response.StatusDescription = exception.Message;
        ////        }
        ////    }
        ////    else
        ////    {
        ////        var controllerName = this.ControllerContext.RouteData.Values["Controller"].ToString();
        ////        var actionName = this.ControllerContext.RouteData.Values["Action"].ToString();
        ////        this.View("Error", new HandleErrorInfo(filterContext.Exception, controllerName, actionName)).ExecuteResult(this.ControllerContext);
        ////    }

        ////    filterContext.ExceptionHandled = true;
        ////}

        private SystemMessageCollection PrepareSystemMessages()
        {
            // Warning: always escape data to prevent XSS
            var messages = new SystemMessageCollection();

            if (this.TempData.ContainsKey(GlobalConstants.InfoMessage))
            {
                messages.Add(this.TempData[GlobalConstants.InfoMessage].ToString(), SystemMessageType.Informational, 1000);
            }

            if (this.TempData.ContainsKey(GlobalConstants.DangerMessage))
            {
                messages.Add(this.TempData[GlobalConstants.DangerMessage].ToString(), SystemMessageType.Error, 1000);
            }

            if (this.TempData.ContainsKey(GlobalConstants.WariningMessage))
            {
                messages.Add(this.TempData[GlobalConstants.WariningMessage].ToString(), SystemMessageType.Warning, 1000);
            }

            if (this.TempData.ContainsKey(GlobalConstants.SuccessMessage))
            {
                messages.Add(this.TempData[GlobalConstants.SuccessMessage].ToString(), SystemMessageType.Success, 1000);
            }

            return messages;
        }
    }
}