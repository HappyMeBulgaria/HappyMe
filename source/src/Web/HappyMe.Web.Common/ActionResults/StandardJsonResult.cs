namespace HappyMe.Web.Common.ActionResults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using HappyMe.Common.Constants;
    using HappyMe.Web.Common.ActionResults.Models;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    internal class StandardJsonResult : JsonResult
    {
        private readonly ICollection<string> errorMessages = new List<string>();

        public StandardJsonResult(object value)
            : base(value)
        {
        }

        public StandardJsonResult(object value, JsonSerializerSettings serializerSettings)
            : base(value, serializerSettings)
        {
        }

        public void AddError(string errorMessage)
        {
            this.errorMessages.Add(errorMessage);
        }

        public override void ExecuteResult(ActionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? ContentTypeConstants.Json : this.ContentType;

            this.SerializeData(response);
        }

        protected virtual void SerializeData(HttpResponse response)
        {
            if (this.errorMessages.Any())
            {
                var originalData = this.Value;
                this.Value = new JsonResponse
                {
                    OriginalData = originalData,
                    ErrorMessages = this.errorMessages
                };

                // Set error response status code if it's not already set
                if (response.StatusCode < (int)HttpStatusCode.BadRequest)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] { new StringEnumConverter(), },
                NullValueHandling = NullValueHandling.Ignore,
            };

            var serializedData = JsonConvert.SerializeObject(this.Value, settings);
            response.WriteAsync(serializedData);
        }
    }
}
