using AutoFixture.NUnit3;
using Moq;
using RentCRL.Application.Properties;
using RentCRL.Domain;
using RentCRL.Domain.Base;
using RentCRL.Domain.Properties;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Application.Tests.Unit.Properties
{
    public class PropertyServiceTests
    {
        private Mock<IGuidProvider> _guidProviderMock;
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        private PropertyService _propertyService;

        [SetUp]
        public void SetUp() 
        { 
            _guidProviderMock = new Mock<IGuidProvider>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _propertyService = new PropertyService(_guidProviderMock.Object, _propertyRepositoryMock.Object);

        }

        [Test, AutoData]
        public async Task DeletePropertyByIdAsync_PropertyExist_DeleteProperty(Guid id)
        {
            var property = PropertyBuilder.Build()
                .WithId(id)
                .Create();

            _propertyRepositoryMock
               .Setup(r => r.GetByIdAsync(id))
               .ReturnsAsync(property);

            var response = await _propertyService.DeletePropertyByIdAsync(id);

            _propertyRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
            property.Id.ShouldBe(id);
            response.IsSuccess.ShouldBeTrue();
        }
        

        [Test, AutoData]
        public async Task DeletePropertyByIdAsync_PropertyNotExist_ReturnSuccess(Guid id)
        {
            _propertyRepositoryMock
               .Setup(r => r.GetByIdAsync(id))
               .ReturnsAsync((Property) null);

            var response = await _propertyService.DeletePropertyByIdAsync(id);

            _propertyRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Never);
            response.IsSuccess.ShouldBeTrue();
        }

        [Test, AutoData]
        public async Task CreatePropertyAsync_PropertyNotExist_CreateProperty(Guid id, string name, decimal surface, string status, Address address, Guid ownerId)
        {
            // Arrange
            _guidProviderMock
                .Setup(p => p.NewGuid())
                .Returns(id);

            Property propertyCreated = null;

            _propertyRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Property>()))
                .Callback((Property property) => { propertyCreated = property; })
                .ReturnsAsync(propertyCreated);

            // Act
            await _propertyService.CreatePropertyAsync(name, surface, status, address, ownerId);

            // Assert
            propertyCreated.ShouldNotBeNull();
            propertyCreated.Id.ShouldBe(id);
            propertyCreated.Name.ShouldBe(name);
            propertyCreated.Surface.ShouldBe(surface);
            propertyCreated.Status.ShouldBe(status);
            propertyCreated.Address.ShouldBe(address);
            propertyCreated.OwnerId.ShouldBe(ownerId);
        }

        [Test, AutoData]
        public async Task GetPropertiesByOwnerIdAsync_PropertyExist_ReturnProperty(Guid ownerId)
        {
            var property = PropertyBuilder
                .Build()
                .WithOwnerId(ownerId)
                .Create();

            var properties = new List<Property> { property };

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesByOwnerIdAsync(ownerId))
                .ReturnsAsync(properties);

            var response = await _propertyService.GetPropertiesByOwnerIdAsync(ownerId);

            response.Value.ShouldBe(properties);
        }

        [Test, AutoData]
        public async Task GetPropertiesByOwnerIdAsync_PropertyNotExist_ReturnFailure(Guid ownerId)
        {
            var properties = new List<Property>();

            _propertyRepositoryMock
                .Setup(r => r.GetPropertiesByOwnerIdAsync(ownerId))
                .ReturnsAsync(properties);

            var response = await _propertyService.GetPropertiesByOwnerIdAsync(ownerId);

           response.IsSuccess.ShouldBeFalse();
           response.Error.ShouldBe(PropertyErrors.CouldNotFoundPropertiesByOwnerId);
        }
    }
}
