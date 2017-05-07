using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HappyMe.Web.Common.ActionResults
{
    using System;

    using HappyMe.Common.Constants;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonCamelCaseResult : ActionResult
    {
        public JsonCamelCaseResult(object data)
        {
            this.Data = data;
        }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public override void ExecuteResult(ActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            var response = actionContext.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : ContentTypeConstants.Json;

            if (this.Data == null)
            {
                return;
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            response.WriteAsync(JsonConvert.SerializeObject(this.Data, jsonSerializerSettings));
        }
    }
}