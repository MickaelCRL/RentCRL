using AutoFixture;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Infrastructure.Tests.Integration.Users
{
    public class OwnerRepositoryTests : BaseTests
    {
        private Fixture _fixture;
        private OwnerRepository _ownerRepository;

        [SetUp]
        public async Task SetUp()
        {
            await BaseSetUp();
            _fixture = new Fixture();
            _ownerRepository = new OwnerRepository(_cosmosDbTestEnvironment.Settings, _cosmosDbTestEnvironment.Client);
        }

        [Test]
        public async Task GetByEmailAsync_OwnerExists_ReturnsOwner()
        {
            var email = _fixture.CreateEmail();

            var unrelatedOwner1 = OwnerBuilder.Build().Create();
            var unrelatedOwner2 = OwnerBuilder.Build().Create();
            var expectedOwner = OwnerBuilder.Build()
                .WithEmail(email)
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedOwner1);
            await container.CreateItemAsync(unrelatedOwner2);
            await container.CreateItemAsync(expectedOwner);

            var result = await _ownerRepository.GetByEmailAsync(email);

            result.ShouldBeEquivalentTo(expectedOwner);
        }

        [Test]
        public async Task GetByEmailAsync_EntityTypeIsNotOwner_ReturnsNull()
        {
            var email = _fixture.CreateEmail();

            var unrelatedOwner1 = OwnerBuilder.Build().Create();
            var unrelatedOwner2 = OwnerBuilder.Build().Create();
            var expectedOwner = UserBuilder.Build()
                .WithEmail(email)
                .WithUsertype(nameof(User))
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedOwner1);
            await container.CreateItemAsync(unrelatedOwner2);
            await container.CreateItemAsync(expectedOwner);

            var result = await _ownerRepository.GetByEmailAsync(email);

            result.ShouldBeNull();
        }

        [Test]
        public async Task GetByEmailAsync_OwnerNotExist_ReturnsNull()
        {
            var email = _fixture.CreateEmail();

            var result = await _ownerRepository.GetByEmailAsync(email);

            result.ShouldBeNull();
        }
    }
}
