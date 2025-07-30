using AutoFixture.NUnit3;
using RentCRL.Infrastructure.Properties;
using RentCRL.Tests.Utils;

namespace RentCRL.Infrastructure.Tests.Integration
{
    public class PropertyRepositoryTests : BaseTests
    {
        private PropertyRepository _propertyRepository;

        [SetUp]
        public void Setup() 
        {
            _propertyRepository = new PropertyRepository(_cosmosDbTestEnvironment.Settings, _cosmosDbTestEnvironment.Client);
        }

        [Test, AutoData]
        public void GetPropertiesByOwnerIdAsync_PropertyExists_ReturnsProperty(Guid ownerId)
        {
            var property1 = PropertyBuilder.Build().Create();
            var property2 = PropertyBuilder.Build().Create();
            var propertyWithOwnerId = PropertyBuilder.Build()
                .WithOwnerId(ownerId)
                .Create();
        }

        [Test]
        public void GetPropertiesByOwnerIdAsync_PropertyNotExist_ReturnsNull()
        {

        }
    }
}
