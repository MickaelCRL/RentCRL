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

        [Test, AutoData]
        public void Constructor_EmptyId_ThrowArgumentException
        (
            string auth0Id,
            string firstName,
            string lastName
        )
        {
            var id = Guid.Empty;
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();

            // Act
            var action = () =>
            {
                var user = new User
                (
                    id,
                    auth0Id,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    nameof(Owner)
                );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyAuth0Id_ThrowArgumentException
        (
            Guid id,
            string firstName,
            string lastName
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();
            var auth0Id = string.Empty;

            // Act 
            var action = () =>
            {
                var user = new User
                (
                    id,
                    auth0Id,
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    nameof(User)
                );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyFirstName_ThrowArgumentException
        (
            Guid id,
            string auth0Id,
            string lastName
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();
            var firstName = string.Empty;

            // Act
            var action = () =>
            {
                var user = new User
               (
                   id,
                   auth0Id,
                   firstName,
                   lastName,
                   email,
                   phoneNumber,
                   nameof(User)
               );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyLastName_ThrowArgumentException
         (
            Guid id,
            string auth0Id,
            string firstName
        )
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();
            var lastName = string.Empty;

            // Act
            var action = () =>
            {
                var user = new User
               (
                   id,
                   auth0Id,
                   firstName,
                   lastName,
                   email,
                   phoneNumber,
                   nameof(User)
               );
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
            var id = Guid.NewGuid();
            var auth0Id = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var phoneNumber = _fixture.CreatePhoneNumber();

            // Act
            var action = () =>
            {
                var user = new User
               (
                   id,
                   auth0Id,
                   firstName,
                   lastName,
                   email,
                   phoneNumber,
                   nameof(User)
               );
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
            var id = Guid.NewGuid();
            var auth0Id = _fixture.Create<string>();
            var firstName = _fixture.Create<string>();
            var lastName = _fixture.Create<string>();
            var email = _fixture.CreateEmail();

            // Act
            var action = () =>
            {
                var user = new User
                 (
                     id,
                     auth0Id,
                     firstName,
                     lastName,
                     email,
                     phoneNumber,
                     nameof(User)
                 );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_NullOrEmptyUserType_ThrowArgumentException
        (
             Guid id,
             string auth0Id,
             string firstName,
             string lastName,
             string email,
             string phoneNumber
        )
        {
            var userType = string.Empty;

            // Act
            var action = () =>
            {
                var user = new User
               (
                   id,
                   auth0Id,
                   firstName,
                   lastName,
                   email,
                   phoneNumber,
                   userType
               );
            };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Test, AutoData]
        public void Constructor_EntityTypeNotInUserTypes_ThrowArgumentException(
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
