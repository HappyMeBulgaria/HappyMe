namespace Te4Fest.Services.Data.Contracts
{
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Common;

    public interface IUsersDataService : IService
    {
        User GetUserByUsername(string username);
    }
}
