namespace HappyMe.Web.Common.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;

    using Microsoft.AspNetCore.Html;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
    using Microsoft.AspNetCore.Routing;

    public static class CheckBoxListHtmlHelperExtensions
    {
        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static IHtmlContent CheckBoxListFor<TModel, TValue>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);

            // TODO: [Migration Fix] uncomment
            // var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            // items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, htmlAttributes);
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        public static IHtmlContent CheckBoxList(this IHtmlHelper htmlHelper, string listName, IEnumerable<SelectListItem> items, object htmlAttributes = null)
        {
            var container = new TagBuilder("div");
            foreach (var item in items)
            {
                var label = new TagBuilder("label");
                label.MergeAttribute("class", "checkbox"); // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);
                if (item.Selected)
                {
                    cb.MergeAttribute("checked", "checked");
                }

                label.InnerHtml.AppendHtml(cb);

                container.InnerHtml.AppendHtml(label);
            }

            return new HtmlString(container.ToString());
        }

        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(object defaultValues, IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
            {
                return selectList;
            }

            var values =
                defaultValuesList.Cast<object>().Select(value => Convert.ToString(value, CultureInfo.CurrentCulture));

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            foreach (var selectListItem in selectList)
            {
                selectListItem.Selected = (selectListItem.Value != null)
                    ? selectedValues.Contains(selectListItem.Value)
                    : selectedValues.Contains(selectListItem.Text);
                newSelectList.Add(selectListItem);
            }

            return newSelectList;
        }
    }
}
