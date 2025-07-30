using AutoFixture;
using AutoFixture.NUnit3;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Domain.Tests.Unit.Users
{
    [TestFixture]
    public class UserTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
        }

        #region Constructor
        [Test, AutoData]
        public void Constructor_ValidArgument_CreateUser
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName
        )
        {
            // Arrange
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            // Act
            var user = new User(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(User));

            // Assert
            user.ShouldNotBeNull();
            user.Id.ShouldBe(id);
            user.Auth0Id.ShouldBe(auth0Id);
            user.FirstName.ShouldBe(firstName);
            user.LastName.ShouldBe(lastName);
            user.Email.ShouldBe(email);
            user.EntityType.ShouldBe(nameof(User));
            user.PhoneNumber.ShouldBe(phoneNumber);
        }

        [Test]
        public void Constructor_EmptyId_ThrowArgumentException()
        {
            // Act
            var action = () =>
            {
                var owner = UserBuilder.Build()
                                          .WithId(Guid.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_NullOrEmptyAuth0Id_ThrowArgumentException()
        {
            // Act 
            var action = () =>
            {
                var user = UserBuilder.Build()
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
                var owner = UserBuilder.Build()
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
                var owner = UserBuilder.Build()
                                          .WithLastName(string.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestCase("")]
        [TestCase("invalidEmail")]
        [TestCase("@invaildEmail.com")]
        public void Constructor_InvalidEmail_ThrowArgumentException
        (
            string email
        )
        {
            // Act
            var action = () =>
            {
                var owner = UserBuilder.Build()
                                          .WithEmail(email)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [TestCase("+toto")]
        [TestCase("skululu")]
        public void Constructor_InvalidPhoneNumber_ThrowArgumentException
        (
            string phoneNumber
        )
        {
            // Act
            var action = () =>
            {
                var owner = UserBuilder.Build()
                          .WithPhoneNumber(phoneNumber)
                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Constructor_NullOrEmptyUserType_ThrowArgumentException()
        {
            // Act
            var action = () =>
            {
                var owner = UserBuilder.Build()
                                          .WithUsertype(string.Empty)
                                          .Create();
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_IfEntity_ThrowArgumentException(
            Guid id,
            string auth0Id,
            string firstName,
            string lastName,
            string entityType
        )
        {
            // Arrange
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            // Act
            var action = () =>
            {
                var owner = new User(
                    id,
                    auth0Id,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    entityType
                );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
        #endregion
    }
}
