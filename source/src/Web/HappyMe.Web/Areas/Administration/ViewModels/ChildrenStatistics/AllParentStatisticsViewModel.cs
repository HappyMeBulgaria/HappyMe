namespace HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics
{
    using System.Collections.Generic;

    using HappyMe.Services.Administration.Models;

    public class AllParentStatisticsViewModel
    {
        public AllParentStatisticsViewModel()
        {
            this.ModulePlayedTimesStatisticsFull = new List<ModulePlayedTimesStatisticFull>();
        }

        public IEnumerable<ModulePlayedTimesStatisticFull> ModulePlayedTimesStatisticsFull { get; set; }
    }
}