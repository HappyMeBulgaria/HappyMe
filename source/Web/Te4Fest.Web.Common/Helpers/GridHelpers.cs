namespace Te4Fest.Web.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using NonFactors.Mvc.Grid;

    public static class GridHelper
    {
        public static IHtmlGrid<TModel> MvcGrid<TModel>(this HtmlHelper helper, IEnumerable<TModel> source, Action<IGridColumns<TModel>> columns) 
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
                    p.InitialPage = 1;
                    p.RowsPerPage = 10;
                });
        }
    }
}
