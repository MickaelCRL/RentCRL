using AutoFixture;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Infrastructure.Tests.Integration.Users
{
    public class UserRepositoryTests : BaseTests
    {
        private Fixture _fixture;
        private UserRepository _userRepository;

        [SetUp]
        public async Task Setup()
        {
            await BaseSetUp();

            _fixture = new Fixture();
            _userRepository = new UserRepository(_cosmosDbTestEnvironment.Settings, _cosmosDbTestEnvironment.Client);
        }

        [Test]
        public async Task GetByEmailAsync_UserExistsWithTypeOwner_ReturnsUser()
        {
            // Arrange
            var email = _fixture.CreateEmail();

            var user1 = UserBuilder.Build().Create();
            var user2 = UserBuilder.Build().Create();
            var userWithEmail = UserBuilder.Build()
                .WithEmail(email)
                .WithUsertype(nameof(Owner))
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(user1);
            await container.CreateItemAsync(user2);
            await container.CreateItemAsync(userWithEmail);

            // Act
            var user = await _userRepository.GetByEmailAsync(email);

            // Assert
            user.ShouldBeEquivalentTo(userWithEmail);
        }

        [Test]
        public async Task GetByEmailAsync_UserExistsWithTypeTenant_ReturnsUser()
        {
            // Arrange
            var email = _fixture.CreateEmail();

            var user1 = UserBuilder.Build().Create();
            var user2 = UserBuilder.Build().Create();
            var userWithEmail = UserBuilder.Build()
                .WithEmail(email)
                .WithUsertype(nameof(Tenant))
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(user1);
            await container.CreateItemAsync(user2);
            await container.CreateItemAsync(userWithEmail);

            // Act
            var user = await _userRepository.GetByEmailAsync(email);

            // Assert
            user.ShouldBeEquivalentTo(userWithEmail);
        }

        [Test]
        public async Task GetByEmailAsync_UserDoesNotExist_ReturnsNull()
        {
            var email = _fixture.CreateEmail();

            var user = await _userRepository.GetByEmailAsync(email);

            user.ShouldBeNull();           
        }
    }
}
