namespace HappyMe.Services.Administration
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Administration.Contracts;
    using HappyMe.Services.Administration.Models;

    public class ChildrenStatisticsService : IChildrenStatisticsService
    {
        private readonly IRepository<UserAnswer> userAnswersRepository;

        public ChildrenStatisticsService(IRepository<UserAnswer> userAnswersRepository)
        {
            this.userAnswersRepository = userAnswersRepository;
        }

        public IQueryable<ChildAnswerRatoStatistic> GetWrongRightAnswersStatistics(string childId)
        {
            return this.userAnswersRepository.All()
                    .Where(x => x.UserId == childId)
                    .GroupBy(x => new { x.ModuleSession.Module.Name, x.Answer.IsCorrect })
                    .Select(x => new ChildAnswerRatoStatistic
                    {
                        ModuleName = x.Key.Name,
                        AnswerStatus = x.Key.IsCorrect,
                        Count = x.Count()
                    });
        }

        public IQueryable<ModuleSessionStatistic> GetModuleSessionStatistics(string childId)
        {
            return
                this.userAnswersRepository.All()
                    .Include(x => x.ModuleSession)
                    .Include(x => x.ModuleSession.Module)
                    .Where(x => x.UserId == childId && x.ModuleSession.FinishDate != null)
                    .ToList()
                    .AsQueryable()
                    .GroupBy(x => x.ModuleSession.Module.Name)
                    .Select(x => new ModuleSessionStatistic
                    {
                        ModuleName = x.Key,
                        AverageTime = new TimeSpan(Convert.ToInt64(x.Select(y => y.ModuleSession.FinishDate.Value - y.ModuleSession.StartedDate).Average(z => z.Ticks))).TotalMinutes
                    });
        }

        public IQueryable<ModulePlayedTimesStatistic> GetModulePlayedTimesStatistics(string childId)
        {
            return
                this.userAnswersRepository.All()
                    .Where(x => x.UserId == childId)
                    .GroupBy(x => x.ModuleSession.Module.Name)
                    .Select(x => new ModulePlayedTimesStatistic { ModuleName = x.Key, TimesPlayed = x.Count() });
        }

        public IQueryable<ModulePlayedTimesStatisticFull> GetModulePlayedTimesStatisticsForParentsChildren(string parentId)
        {
            return
                this.userAnswersRepository.All()
                    .GroupBy(x => new { x.User.UserName, x.ModuleSession.Module.Name })
                    .Select(x => new ModulePlayedTimesStatisticFull { ModuleName = x.Key.Name, ChildName = x.Key.UserName, TimesPlayed = x.Count() });
        }
    }
}
