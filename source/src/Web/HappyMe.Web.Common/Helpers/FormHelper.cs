namespace HappyMe.Web.Common.Helpers
{
    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class FormHelper
    {
        private const string DefaultArea = "Administration";

        public static IHtmlContent DeleteForm(this IHtmlHelper helper, string action, string controller, string id, string secondId = null, string area = DefaultArea)
        {
            ////var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            ////var url = urlHelper.Action(action, controller, new { area });

            var form = new TagBuilder("form");
            form.Attributes.Add("action", string.Empty);
            form.Attributes.Add("method", "POST");
            form.AddCssClass("delete-form");

            var antiForgeryToken = helper.AntiForgeryToken();
            var hiddenId = new TagBuilder("input");
            hiddenId.Attributes.Add("type", "hidden");
            hiddenId.Attributes.Add("name", "id");
            hiddenId.Attributes.Add("value", id);

            var submitInput = new TagBuilder("input");
            submitInput.Attributes.Add("type", "submit");
            submitInput.Attributes.Add("value", "Изтрий");
            submitInput.Attributes.Add("class", "btn btn-warning");

            if (!string.IsNullOrWhiteSpace(secondId))
            {
                var secondHiddenId = new TagBuilder("input");
                secondHiddenId.Attributes.Add("type", "hidden");
                secondHiddenId.Attributes.Add("name", "secondId");
                secondHiddenId.Attributes.Add("value", secondId);
                form.InnerHtml.AppendHtml(secondHiddenId);
            }

            form.InnerHtml.AppendHtml(antiForgeryToken);
            form.InnerHtml.AppendHtml(hiddenId);
            form.InnerHtml.AppendHtml(submitInput);

            return new HtmlString(form.ToString());
        }
    }
}
