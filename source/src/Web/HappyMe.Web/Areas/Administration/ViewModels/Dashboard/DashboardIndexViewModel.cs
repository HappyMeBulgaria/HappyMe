namespace HappyMe.Web.Areas.Administration.ViewModels.Dashboard
{
    using System.Collections.Generic;

    public class DashboardIndexViewModel
    {
        public IEnumerable<ChildViewModel> CurrentUserChildren { get; set; }
    }
}
