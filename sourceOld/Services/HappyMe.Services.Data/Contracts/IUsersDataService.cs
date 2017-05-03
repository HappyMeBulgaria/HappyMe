namespace HappyMe.Services.Data.Contracts
{
    using HappyMe.Data.Models;
    using HappyMe.Services.Common;

    public interface IUsersDataService : IService
    {
        User GetUserByUsername(string username);
    }
}
