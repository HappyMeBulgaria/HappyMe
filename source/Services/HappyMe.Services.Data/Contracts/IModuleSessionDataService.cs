namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using HappyMe.Data.Models;

    public interface IModuleSessionDataService
    {
        ModuleSession GetById(int id);

        Question NextQuestion(int moduleSessionId, string userId);

        Task<int> StartAnonymousSession(int moduleId);

        Task<int> StartUserSession(string userId, int moduleId);
    }
}
