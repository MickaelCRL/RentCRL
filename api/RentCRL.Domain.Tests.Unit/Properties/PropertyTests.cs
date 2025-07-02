using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Properties;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Properties
{
    [TestFixture]
    public class PropertyTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
        }
        #region Constructor
        [Test, AutoData]
        public void Constructor_ValidArgument_CreateProperty
        (
            string name,
            decimal surface,
            string status,
            Address address,
            Guid ownerId
        )
        {
            // Arrange

            // Act
            var property = new Property(name, surface, status, address, ownerId);

            // Assert
            property.ShouldNotBeNull();
            property.Name.ShouldBe(name);
            property.Surface.ShouldBe(surface);
            property.Status.ShouldBe(status);
            property.Address.ShouldBe(address);
        }

        [Test]
        public void Constructor_NullOrEmptyName_ThrowArgumentException()
        {
            // Act 
            var action = () =>
            {
                var property = PropertyBuilder.Build()
                                              .WithName(string.Empty)
                                              .Create();
            };
            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_NegativeOrZeroSurface_ThrowArgumentException()
        {
            // Act 
            var action = () =>
            {
                var property = PropertyBuilder.Build()
                                              .WithSurface(0)
                                              .Create();
            };
            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_EmptyOwnerId_ThrowArgumentException()
        {
            // Act 
            var action = () =>
            {
                var property = PropertyBuilder.Build()
                                              .WithOwnerId(Guid.Empty)
                                              .Create();
            };
            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        #endregion
    }
}
