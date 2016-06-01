namespace HappyMe.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using HappyMe.Data.Models;

    public interface IModuleSessionDataService
    {
        Question NextQuestion(int moduleSessionId, string userId);

        Task StartAnonymousSession(int moduleId);

        Task StartUserSession(string userId, int moduleId);
    }
}
