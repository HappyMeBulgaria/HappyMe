namespace HappyMe.Services.Data.Test
{
    using System;

    using HappyMe.Data.Contracts.Repositories;
    using HappyMe.Data.Contracts.Repositories.Contracts;
    using HappyMe.Data.Models;
    using HappyMe.Services.Data;

    using Xunit;

    public class UsersDataServiceTests
    {
        private readonly IDeletableEntityRepository<User> usersRepository;
        private readonly UsersDataService usersDataService;

        public UsersDataServiceTests()
        {
            this.usersRepository = new InMemoryDeletableEntityRepository<User, string>();
            this.usersDataService = new UsersDataService(this.usersRepository);
        }

        [Fact]
        public void GetUserByUsername_ShouldThrowExceptionIfInvalidUsernameIsGiven()
        {
            Assert.Throws<ArgumentNullException>(() => this.usersDataService.GetUserByUsername(null));
            Assert.Throws<ArgumentNullException>(() => this.usersDataService.GetUserByUsername(string.Empty));
            Assert.Throws<ArgumentNullException>(() => this.usersDataService.GetUserByUsername(new string(' ', 20)));
        }

        [Fact]
        public void GetUserByUsername_ShouldReturnUserIfExistingUsernameIsGiven()
        {
            var fakeUsername = "FakeMe";
            var fakeUser = new User
            {
                UserName = fakeUsername
            };
            
            this.usersRepository.Add(fakeUser);

            var searchResult = this.usersDataService.GetUserByUsername(fakeUsername);

            Assert.NotNull(searchResult);
            Assert.Equal(fakeUsername, searchResult.UserName);
        }

        [Fact]
        public void GetUserByUsername_ShouldReturnNullIfNonExistingUsernameIsGiven()
        {
            var fakeUsername = "FakeMe";
            var fakeUser = new User
            {
                UserName = fakeUsername
            };

            this.usersRepository.Add(fakeUser);

            var searchResult = this.usersDataService.GetUserByUsername("NoUserName");

            Assert.Null(searchResult);
        }
    }
}
