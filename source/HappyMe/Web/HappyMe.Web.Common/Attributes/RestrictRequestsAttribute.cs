using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HappyMe.Web.Common.Attributes
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Security.Cryptography;
    using System.Text;

    using HappyMe.Web.Common.Extensions;
    using Microsoft.Extensions.Caching.Memory;

    public class RestrictRequestsAttribute : ActionFilterAttribute
    {
        // The default Error Message that will be displayed in case of excessive Requests
        private string defaultErrorMessage = "Моля, изчакайте 10 секунди преди да пробвате пак.";

        // This stores the time between Requests (in seconds)
        private int restrictInterval = 60;

        // Max number of request per interval
        private int requestsPerInterval = 5;

        // This will store the URL to Redirect errors to
        // NOTE: Not implemented.
        //// private string redirectURL;

        /// <summary>
        /// Number of request before the protection is turned on
        /// </summary>
        public int RequestsPerInterval
        {
            get { return this.requestsPerInterval; }
            set { this.requestsPerInterval = value; }
        }

        public string ErrorMessage
        {
            get
            {
                var resourceProperty = this.ResourceType != null ? this.ResourceType.GetProperty(this.ResourceName, BindingFlags.Static | BindingFlags.Public) : null;
                if (resourceProperty != null)
                {
                    return (string)resourceProperty.GetValue(resourceProperty.DeclaringType, null);
                }

                return this.defaultErrorMessage;
            }

            set
            {
                this.defaultErrorMessage = value;
            }
        }

        public Type ResourceType { get; set; }

        public string ResourceName { get; set; }

        /// <summary>
        /// Restrict interval in seconds
        /// </summary>
        public int RestrictInterval
        {
            get { return this.restrictInterval; }
            set { this.restrictInterval = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.IsAdmin())
            {
                // Store our HttpContext (for easier reference and code brevity)
                var request = filterContext.HttpContext.Request;

                //TODO: Use IMemory Cache to implement this attribute: http://stackoverflow.com/questions/34857145/cache-asp-net-doesnt-exist-asp-net-5

                // Store our HttpContext.Cache (for easier reference and code brevity)
                //var cache = filterContext.HttpContext.Cache;

                //// Grab the IP Address from the originating Request (very simple implementation for example purposes)
                //// and append the User Agent
                //var originationInfo = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress + request.UserAgent;

                //// Now we just need the target URL Information
                //var targetInfo = request.RawUrl + request.QueryString;

                //// Generate a hash for your strings (this appends each of the bytes of the value into a single hashed string
                //var hashValue = string.Join(
                //    string.Empty,
                //    MD5.Create()
                //        .ComputeHash(Encoding.ASCII.GetBytes(originationInfo + targetInfo))
                //        .Select(s => s.ToString("x2")));

                //// Checks if the hashed value is contained in the Cache (indicating a repeat request)
                //if (cache[hashValue] != null)
                //{
                //    // Converts the cache into a int to check the number of request to this point
                //    var cacheValue = int.Parse(cache[hashValue].ToString());

                //    if (cacheValue >= this.RequestsPerInterval)
                //    {
                //        // Adds the Error Message to the Model and Redirect
                //        (filterContext.Controller as ControllerContext).ModelState.AddModelError(string.Empty, this.ErrorMessage);
                //    }
                //    else
                //    {
                //        // Increments the number of requests
                //        cacheValue++;
                //        cache.Remove(hashValue);
                //        cache.Add(hashValue, cacheValue, null, DateTime.Now.AddSeconds(this.RestrictInterval), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                //    }
                //}
                //else
                //{
                //    // Adds an 1 int (representing the first request) to the cache using the hashValue to a key 
                //    // (This sets the expiration that will determine if the Request is valid or not)
                //    cache.Add(hashValue, 1, null, DateTime.Now.AddSeconds(this.RestrictInterval), Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
                //}
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
