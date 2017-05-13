namespace HappyMe.Web.Common.Helpers
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using NonFactors.Mvc.Grid;

    public static class GridHelper
    {
        public static IHtmlGrid<TModel> MvcGrid<TModel>(
            this HtmlHelper helper,
            IEnumerable<TModel> source,
            Action<IGridColumnsOf<TModel>> columns)
            where TModel : class
        {
            return helper
                .Grid(source)
                .Build(columns)
                .Filterable()
                .Sortable()
                .Pageable(p =>
                {
                    p.PagesToDisplay = 5;
                    p.RowsPerPage = 10;
                });
        }
    }
}
