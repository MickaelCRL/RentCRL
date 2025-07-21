using AutoFixture;
using Moq;
using RentCRL.Application.Users;
using RentCRL.Domain.Results;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Application.Tests.Unit.Users
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private IUserService _userService;
        private Fixture _fixture;
        

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetUserByEmailAsync_UserExists_ReturnUser()
        {
            // Arrange
            var email = _fixture.CreateEmail();
            var user = UserBuilder
                .Build()
                .Create();

            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(email))
                .ReturnsAsync(user);

            // Act
            var response = await _userService.GetUserByEmailAsync(email);

            // Assert 
            response.Value.ShouldBe(user);
        }

        [Test]
        public async Task GetUserByEmailAsync_UserNotExist_ReturnFaillure()
        {
            // arrange
            var email = _fixture.CreateEmail();

            // act
            var response = await _userService.GetUserByEmailAsync(email);

            // assert
            response.IsSuccess.ShouldBe(false);
            response.Error.ShouldBe(UserErrors.CouldNotFindUserWithEmail);
        }
    }
}
