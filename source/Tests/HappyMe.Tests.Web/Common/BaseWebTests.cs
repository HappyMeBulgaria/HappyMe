namespace HappyMe.Tests.Web.Common
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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

        protected HttpContextBase MockHttpContextBase()
        {
            var mockRequest = new Mock<HttpRequestBase>();

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.SetupGet(x => x.Request).Returns(mockRequest.Object);

            return mockHttpContext.Object;
        }
    }
}
