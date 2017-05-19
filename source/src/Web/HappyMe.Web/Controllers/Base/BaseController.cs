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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            this.ViewBag.SystemMessages = this.PrepareSystemMessages();
            base.OnActionExecuting(context);
        }

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
