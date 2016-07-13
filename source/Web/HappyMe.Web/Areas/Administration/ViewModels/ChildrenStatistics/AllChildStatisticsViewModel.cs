namespace HappyMe.Web.Areas.Administration.ViewModels.ChildrenStatistics
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using HappyMe.Services.Administration.Models;

    [DataContract]
    public class AllChildStatisticsViewModel
    {
        public AllChildStatisticsViewModel()
        {
            this.ChildAnswerRatoStatistics = new List<ChildAnswerRatoStatistic>();
            this.ModulePlayedTimesStatistics = new List<ModulePlayedTimesStatistic>();
            this.ModuleSessionStatistics = new List<ModuleSessionStatistic>();
        }

        [DataMember(Name = "childAnswerRatoStatistics")]
        public IEnumerable<ChildAnswerRatoStatistic> ChildAnswerRatoStatistics { get; set; }

        [DataMember(Name = "moduleSessionStatistics")]
        public IEnumerable<ModuleSessionStatistic> ModuleSessionStatistics { get; set; }

        [DataMember(Name = "modulePlayedTimesStatistics")]
        public IEnumerable<ModulePlayedTimesStatistic> ModulePlayedTimesStatistics { get; set; }
    }
}