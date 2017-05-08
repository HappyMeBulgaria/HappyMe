namespace HappyMe.Web.Test.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Moq;

    public class BaseWebTests
    {
        protected void TryValidateModel(object model, Controller controller)
        {
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(model, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }
        }

        protected HttpContext MockHttpContextBase()
        {
            var mockRequest = new Mock<HttpRequest>();
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            return mockHttpContext.Object;
        }
    }
}
