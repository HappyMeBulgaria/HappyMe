namespace HappyMe.Web.Models.Areas.Administration.ViewModels.ChildrenStatistics
{
    using System.Collections.Generic;

    using Services.Administration.Models;

    public class AllParentStatisticsViewModel
    {
        public AllParentStatisticsViewModel()
        {
            this.ModulePlayedTimesStatisticsFull = new List<ModulePlayedTimesStatisticFull>();
        }

        public IEnumerable<ModulePlayedTimesStatisticFull> ModulePlayedTimesStatisticsFull { get; set; }
    }
}