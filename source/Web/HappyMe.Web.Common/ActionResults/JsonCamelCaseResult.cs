namespace HappyMe.Web.Common.ActionResults
{
    using System;
    using System.Text;
    using System.Web.Mvc;

    using HappyMe.Common.Constants;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonCamelCaseResult : ActionResult
    {
        public JsonCamelCaseResult(object data, JsonRequestBehavior jsonRequestBehavior)
        {
            this.Data = data;
            this.JsonRequestBehavior = jsonRequestBehavior;
        }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet
                && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : ContentTypeConstants.Json;
            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data == null)
            {
                return;
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            response.Write(JsonConvert.SerializeObject(this.Data, jsonSerializerSettings));
        }
    }
}