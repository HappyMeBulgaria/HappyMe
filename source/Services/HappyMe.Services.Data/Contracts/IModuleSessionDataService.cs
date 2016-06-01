namespace HappyMe.Services.Data.Contracts
{
    using HappyMe.Data.Models;

    public interface IModuleSessionDataService
    {
        Question NextQuestion(int moduleSessionId, string userId);
    }
}
