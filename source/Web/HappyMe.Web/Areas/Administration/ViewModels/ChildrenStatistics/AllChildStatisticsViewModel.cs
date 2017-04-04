namespace HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics
{
    using System.Collections.Generic;
    using HappyMe.Services.Administration.Models;

    public class AllChildStatisticsViewModel
    {
        public AllChildStatisticsViewModel()
        {
            this.ChildAnswerRatoStatistics = new List<ChildAnswerRatoStatistic>();
            this.ModulePlayedTimesStatistics = new List<ModulePlayedTimesStatistic>();
            this.ModuleSessionStatistics = new List<ModuleSessionStatistic>();
        }

        public IEnumerable<ChildAnswerRatoStatistic> ChildAnswerRatoStatistics { get; set; }

        public IEnumerable<ModuleSessionStatistic> ModuleSessionStatistics { get; set; }

        public IEnumerable<ModulePlayedTimesStatistic> ModulePlayedTimesStatistics { get; set; }
    }
}