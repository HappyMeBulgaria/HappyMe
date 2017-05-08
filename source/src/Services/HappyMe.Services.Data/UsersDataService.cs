namespace HappyMe.Services.Data
{
    using System;
    using System.Linq;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data.Contracts;

    public class UsersDataService : IUsersDataService
    {
        private readonly IDeletableEntityRepository<User> usersRepository;

        public UsersDataService(IDeletableEntityRepository<User> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public User GetUserByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username), "Invalid username!");
            }

            return this.usersRepository.All().FirstOrDefault(u => u.UserName == username);
        }
    }
}
