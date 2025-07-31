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

            var expectedResult = new List<Property> { expectedProperty1, expectedProperty2 };

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedProperty1);
            await container.CreateItemAsync(unrelatedProperty2);
            await container.CreateItemAsync(expectedProperty1);
            await container.CreateItemAsync(expectedProperty2);

            var result = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);
           
            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test, AutoData]
        public async Task GetPropertiesByOwnerIdAsync_PropertyNotExist_ReturnsListEmpty(Guid ownerId)
        {
            var result = await _propertyRepository.GetPropertiesByOwnerIdAsync(ownerId);

            result.ShouldBeEmpty();
        }
    }
}
