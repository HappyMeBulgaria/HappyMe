namespace HappyMe.Services.Administration.Contracts
{
    using System.Linq;

    using HappyMe.Services.Administration.Models;
    using HappyMe.Services.Common;

    public interface IChildrenStatisticsService : IService
    {
        IQueryable<ChildAnswerRatoStatistic> GetWrongRightAnswersStatistics(string childId);

        IQueryable<ModuleSessionStatistic> GetModuleSessionStatistics(string childId);

        IQueryable<ModulePlayedTimesStatistic> GetModulePlayedTimesStatistics(string childId);

        IQueryable<ModulePlayedTimesStatisticFull> GetModulePlayedTimesStatisticsForParentsChildren(string parentId);
    }
}
