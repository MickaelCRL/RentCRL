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
            _ownerRepository = new OwnerRepository(_environment.Settings, _environment.Client);
        }

        [Test]
        public async Task GetByEmailAsync_OwnerExists_ReturnsOwner()
        {
            var email = _fixture.CreateEmail();

            var owner1 = OwnerBuilder.Build().Create();
            var owner2 = OwnerBuilder.Build().Create();
            var ownerWithEmail = OwnerBuilder.Build()
                .WithEmail(email)
                .Create();

            var container = _service.GetEntitiesContainer();

            await container.CreateItemAsync(owner1);
            await container.CreateItemAsync(owner2);
            await container.CreateItemAsync(ownerWithEmail);

            var owner = await _ownerRepository.GetByEmailAsync(email);

            owner.ShouldBeEquivalentTo(ownerWithEmail);
        }

        [Test]
        public async Task GetByEmailAsync_OwnerNotExist_ReturnsNull()
        {
            var email = _fixture.CreateEmail();

            var owner = await _ownerRepository.GetByEmailAsync(email);

            owner.ShouldBeNull();
        }
    }
}
