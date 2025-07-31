using AutoFixture.NUnit3;
using RentCRL.Domain.Properties;
using RentCRL.Infrastructure.Properties;
using RentCRL.Tests.Utils;
using Shouldly;
using System.Threading.Tasks;

namespace RentCRL.Infrastructure.Tests.Integration.Properties
{
    public class PropertyRepositoryTests : BaseTests
    {
        private PropertyRepository _propertyRepository;

        [SetUp]
        public async Task Setup() 
        {
            await BaseSetUp();
            _propertyRepository = new PropertyRepository(_cosmosDbTestEnvironment.Settings, _cosmosDbTestEnvironment.Client);
        }

        [Test, AutoData]
        public async Task GetPropertiesByOwnerIdAsync_PropertyExists_ReturnsProperties(Guid ownerId)
        {
            var unrelatedProperty1 = PropertyBuilder.Build().Create();
            var unrelatedProperty2 = PropertyBuilder.Build().Create();

            var expectedProperty1 = PropertyBuilder.Build()
                .WithOwnerId(ownerId)
                .Create();

            var expectedProperty2= PropertyBuilder.Build()
                .WithOwnerId(ownerId)
                .Create();

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedProperty1);
            await container.CreateItemAsync(unrelatedProperty2);
            await container.CreateItemAsync(expectedProperty1);
            await container.CreateItemAsync(expectedProperty2);

            var result = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);

            result.ShouldContain(p => p.Id == expectedProperty1.Id);
            result.ShouldContain(p => p.Id == expectedProperty2.Id);
            result.ShouldAllBe(p => p.OwnerId == ownerId && p.EntityType == nameof(Property));
        }

        [Test, AutoData]
        public async Task GetPropertiesByOwnerIdAsync_PropertyNotExist_ReturnsNull(Guid ownerId)
        {
            var result = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);

            result.ShouldNotBeNull();
        }
    }
}
