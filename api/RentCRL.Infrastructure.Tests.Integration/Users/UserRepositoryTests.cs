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

        [TestCase(nameof(Owner))]
        [TestCase(nameof(Tenant))]
        public async Task GetByEmailAsync_UserExists_ReturnsUser
        (
            string userType
        )
        {
            // Arrange
            var email = _fixture.CreateEmail();

            var unrelatedUser1 = UserBuilder.Build().Create();
            var unrelatedUser2 = UserBuilder.Build().Create();
            var expectedUser = UserBuilder.Build()
                .WithEmail(email)
                .WithUsertype(userType)
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedUser1);
            await container.CreateItemAsync(unrelatedUser2);
            await container.CreateItemAsync(expectedUser);

            // Act
            var result = await _userRepository.GetByEmailAsync(email);

            // Assert
            result.ShouldBeEquivalentTo(expectedUser);
        }

        [Test]
        public async Task GetByEmailAsync_UserDoesNotExist_ReturnsNull()
        {
            var email = _fixture.CreateEmail();

            var result = await _userRepository.GetByEmailAsync(email);

            result.ShouldBeNull();           
        }
    }
}
