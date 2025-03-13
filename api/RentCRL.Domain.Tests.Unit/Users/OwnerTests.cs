using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Users
{
    [TestFixture]
    public class OwnerTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
        }

        #region Constructor
        [Test, AutoData]
        public void Constructor_ValidArgument_CreateOwner(
            string auth0Id,
            string firstName,
            string lastName
            )
        {
            // Arrange
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            // Act
            var owner = new Owner (auth0Id, firstName, lastName, email, phoneNumber);

            // Assert
            owner.ShouldNotBeNull();
            owner.Auth0Id.ShouldBe(auth0Id);
            owner.FirstName.ShouldBe(firstName);
            owner.LastName.ShouldBe(lastName);
            owner.Email.ShouldBe(email);
            owner.PhoneNumber.ShouldBe(phoneNumber);
        }

        [Test]
        public void Constructor_NullOrEmptyAuth0Id_ThrowArgumentException()
        {
            // Act 
            var action = () =>
            {
                var owner = OwnerBuilder.Build()
                                          .WithAuth0Id(string.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_NullOrEmptyFirstName_ThrowArgumentException()
        {
            // Act
            var action = () =>
            {
                var owner = OwnerBuilder.Build()
                                          .WithFirstName(string.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_NullOrEmptyLastName_ThrowArgumentException()
        {
            // Act
            var action = () =>
            {
                var owner = OwnerBuilder.Build()
                                          .WithLastName(string.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestCase("")]
        [TestCase("invalidEmail")]
        [TestCase("@invaildEmail.com")]
        public void Constructor_InvalidEmail_ThrowArgumentException(
            string email
            )
        {
            // Act
            var action = () =>
            {
                var owner = OwnerBuilder.Build()
                                          .WithEmail(email)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestCase("+336000000")]
        [TestCase("33600000000")]
        [TestCase("+toto")]
        [TestCase("skululu")]
        public void Constructor_InvalidPhoneNumber_ThrowArgumentException(
            string phoneNumber
            )
        {
            // Act
            var action = () =>
            {
                var owner = OwnerBuilder.Build()
                          .WithPhoneNumber(phoneNumber)
                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
        #endregion
    }
}
