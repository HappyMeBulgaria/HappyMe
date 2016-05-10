﻿namespace Te4Fest.Web.Common.Extensions
{
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    using Newtonsoft.Json;

    using Te4Fest.Common.Constants;
    using Te4Fest.Web.Common.ActionResults;

    public static class ControllerExtensions
    {
        public static ActionResult EmptyResult(this Controller controller)
        {
            return new EmptyResult();
        }

        public static ActionResult JsonSuccess(
            this Controller controller,
            object data,
            string contentType = null,
            Encoding contentEncoding = null,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            return new StandardJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = jsonRequestBehavior,
            };
        }

        public static JsonResult JsonError(
            this Controller controller,
            string errorMessage,
            string contentType = null,
            Encoding contentEncoding = null,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            var result = new StandardJsonResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = jsonRequestBehavior,
            };

            result.AddError(errorMessage);

            return result;
        }

        public static JsonResult JsonValidation(
            this Controller controller,
            string contentType = null,
            Encoding contentEncoding = null,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            var result = new StandardJsonResult
            {
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = jsonRequestBehavior,
            };

            var validationMessages = controller.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            foreach (var validationMessage in validationMessages)
            {
                result.AddError(validationMessage);
            }

            return result;
        }

        public static ActionResult JsonWithoutReferenceLoop(
            this Controller controller,
            object data,
            Encoding contentEncoding = null)
        {
            var serializationSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(data, Formatting.None, serializationSettings);

            var result = new ContentResult
            {
                Content = json,
                ContentType = ContentTypeConstants.Json,
                ContentEncoding = contentEncoding
            };

            return result;
        }
    }
}
