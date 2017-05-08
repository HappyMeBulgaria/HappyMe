namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using HappyMe.Data.Models;
    using HappyMe.Services.Common;

    public interface IModuleSessionDataService : IService
    {
        ModuleSession GetById(int id);

        Question NextQuestion(int moduleSessionId, string userId);

        void FinishSession(int id);

        Task<ModuleSession> StartAnonymousSession(int moduleId);

        Task<ModuleSession> StartUserSession(string userId, int moduleId);
    }
}
