namespace Te4Fest.Services.Data
{
    using System.Linq;

    using Te4Fest.Data.Contracts.Repositories;
    using Te4Fest.Data.Models;
    using Te4Fest.Services.Data.Contracts;

    public class UsersDataService : IUsersDataService
    {
        private readonly IDeletableEntityRepository<User> usersRepository;

        public UsersDataService(IDeletableEntityRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public User GetUserByUsername(string username)
        {
            return this.usersRepository.All().FirstOrDefault(u => u.UserName == username);
        }
    }
}
