using AutoFixture;
using AutoFixture.NUnit3;
using Moq;
using RentCRL.Application.Users;
using RentCRL.Domain;
using RentCRL.Domain.Base;
using RentCRL.Domain.Users;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Application.Tests.Unit.Users
{
    public class OwnerServiceTests
    {
        private Mock<IGuidProvider> _guidProviderMock;
        private Mock<IOwnerRepository> _ownerRepositoryMock;
        private OwnerService _ownerService;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _guidProviderMock = new Mock<IGuidProvider>();
            _ownerRepositoryMock = new Mock<IOwnerRepository>();
            _ownerService = new OwnerService(_guidProviderMock.Object, _ownerRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Test, AutoData]
        public async Task CreateOwnerAsync_OwnerNotExist_CreateOwner(Guid ownerId, string auth0Id, string firstName, string lastName)
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();
            var address = new Address("12 rue", null, "75000", "Paris", "France");
            _guidProviderMock
                .Setup(p => p.NewGuid())
                .Returns(ownerId);

            Owner ownerCreated = null;

            _ownerRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Owner>()))
                .Callback((Owner owner) => { ownerCreated = owner; })
                .ReturnsAsync((Owner owner) => owner);

            await _ownerService.CreateOwnerAsync(auth0Id, firstName, lastName, email, phoneNumber, address);

            ownerCreated.ShouldNotBeNull();
            ownerCreated.Auth0Id.ShouldBe(auth0Id);
            ownerCreated.FirstName.ShouldBe(firstName);
            ownerCreated.LastName.ShouldBe(lastName);
            ownerCreated.Email.ShouldBe(email);
            ownerCreated.PhoneNumber.ShouldBe(phoneNumber);
            ownerCreated.Address.ShouldBe(address);
        }

        [Test, AutoData]
        public async Task CreateOwnerAsync_OwnerExists_ReturnFailure(Guid ownerId, string auth0Id, string firstName, string lastName)
        {
            var email = _fixture.CreateEmail();
            var phoneNumber = _fixture.CreatePhoneNumber();
            var address = new Address("12 rue", null, "75000", "Paris", "France");
            _guidProviderMock 
                .Setup(p => p.NewGuid())
                .Returns(ownerId);

            var owner = OwnerBuilder
                .Build()
                .WithEmail(email)
                .WithAddress(address)
                .Create();

            _ownerRepositoryMock
                .Setup(r => r.GetByEmailAsync(email))
                .ReturnsAsync(owner);

            var response = await _ownerService.CreateOwnerAsync(auth0Id, firstName, lastName, email, phoneNumber, address);

            response.IsSuccess.ShouldBeFalse();
            response.Error.ShouldBe(UserErrors.EmailAlreadyExists);
        }

        [Test, AutoData]
        public async Task GetOwnerById_OwnerExists_ReturnOwner(Guid id)
        {
            var owner = OwnerBuilder
                .Build()
                .Create();

            _ownerRepositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(owner);

            var response = await _ownerService.GetOwnerByIdAsync(id);

            response.Value.ShouldBe(owner);
        }

        [Test, AutoData]
        public async Task GetOwnerById_OwnerNotExist_ReturnFailure(Guid id)
        {
            Owner owner = null;

            _ownerRepositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(owner);

            var response = await _ownerService.GetOwnerByIdAsync(id);

            response.IsSuccess.ShouldBeFalse();
            response.Error.ShouldBe(UserErrors.CouldNotFindUserWithId);
        }
    }
}
