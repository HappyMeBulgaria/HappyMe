namespace Te4Fest.Services.Data
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Data.Contracts;

    public class UsersDataService : IUsersDataService
    {
        private readonly IDeletableEntityRepository<User> usersData;

        public UsersDataService(IDeletableEntityRepository<User> usersData)
        {
            this.usersData = usersData;
        }

        public User GetUserByUsername(string username)
        {
            return this.usersData.All().FirstOrDefault(u => u.UserName == username);
        }
    }
}
