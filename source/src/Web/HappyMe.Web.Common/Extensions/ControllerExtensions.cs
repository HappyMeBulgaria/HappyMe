namespace HappyMe.Web.Common.Extensions
{
    using System.Linq;

    using HappyMe.Common.Constants;
    using HappyMe.Web.Common.ActionResults;

    using Microsoft.AspNetCore.Mvc;

    using Newtonsoft.Json;

    public static class ControllerExtensions
    {
        public static IActionResult EmptyResult(this Controller controller)
        {
            return new EmptyResult();
        }

        public static IActionResult JsonCamelCase(this Controller controler, object data) =>
            new JsonCamelCaseResult(data);

        public static IActionResult JsonSuccess(this Controller controller, object data) => new StandardJsonResult(data);

        public static IActionResult JsonError(this Controller controller, string errorMessage)
        {
            var result = new StandardJsonResult(null);

            result.AddError(errorMessage);

            return result;
        }

        public static JsonResult JsonValidation(this Controller controller)
        {
            var result = new StandardJsonResult(null);

            var validationMessages = controller.ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
            foreach (var validationMessage in validationMessages)
            {
                result.AddError(validationMessage);
            }

            return result;
        }

        public static ActionResult JsonWithoutReferenceLoop(this Controller controller, object data)
        {
            var serializationSettings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var json = JsonConvert.SerializeObject(data, Formatting.None, serializationSettings);

            var result = new ContentResult
            {
                Content = json,
                ContentType = ContentTypeConstants.Json
            };

            return result;
        }
    }
}
