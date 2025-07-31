using AutoFixture.NUnit3;
using RentCRL.Domain.Properties;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Properties
{
    [TestFixture]
    public class PropertyTests
    {
        #region Constructor
        [Test, AutoData]
        public void Constructor_ValidArgument_CreateProperty
        (
            Guid id,
            string name,
            decimal surface,
            string status,
            Address address,
            Guid ownerId
        )
        {
            // Act
            var property = new Property(id, name, surface, status, address, ownerId);

            // Assert
            property.ShouldNotBeNull();
            property.Name.ShouldBe(name);
            property.Surface.ShouldBe(surface);
            property.Status.ShouldBe(status);
            property.Address.ShouldBe(address);
            property.OwnerId.ShouldBe(ownerId);
            property.EntityType.ShouldBe(nameof(Property));
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyName_ThrowArgumentException
          (
            Guid id,
            decimal surface,
            string status,
            Address address,
            Guid ownerId
        )
        {
            var name = string.Empty;

            // Act 
            var action = () =>
            {
                var property = new Property(id, name, surface, status, address, ownerId);
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NegativeOrZeroSurface_ThrowArgumentException
         (
            Guid id,
            string name,
            string status,
            Address address,
            Guid ownerId
        )
        {
           decimal surface = 0;

           // Act 
           var action = () =>
            {
                var property = new Property(id, name, surface, status, address, ownerId);
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_EmptyOwnerId_ThrowArgumentException
        (
            Guid id,
            string name,
            decimal surface,
            string status,
            Address address
        )
        {
            Guid ownerId = Guid.Empty;

            // Act 
            var action = () =>
            {
                var property = new Property(id, name, surface, status, address, ownerId);
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_EntityType_ReturnProperty
        (
            Guid id,
            string name,
            decimal surface,
            string status,
            Address address,
            Guid ownerId
        )
        {
            var property = new Property(id, name, surface, status, address, ownerId);

            property.EntityType.ShouldBe(nameof(Property));
        }

        #endregion
    }
}
