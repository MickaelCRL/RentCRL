using AutoFixture.NUnit3;
using Moq;
using RentCRL.Application.Contracts;
using RentCRL.Domain.Base;
using RentCRL.Domain.Contracts;
using RentCRL.Tests.Utils;
using Shouldly;

namespace RentCRL.Application.Tests.Unit.Contracts
{
    public class ContractServiceTests
    {
        private Mock<IGuidProvider> _guidProviderMock;
        private Mock<IContractRepository> _contractRepositoryMock;
        private ContractService _contractService;

        [SetUp]
        public void SetUp()
        {
            _guidProviderMock = new Mock<IGuidProvider>();
            _contractRepositoryMock = new Mock<IContractRepository>();
            _contractService = new ContractService(_guidProviderMock.Object, _contractRepositoryMock.Object);
        }

        [Test, AutoData]
        public async Task DeleteContractByIdAsync_ContractExist_DeleteContract(Guid id)
        {
            var contract = ContractBuilder.Build()
                .WithId(id)
                .Create();

            _contractRepositoryMock
               .Setup(r => r.GetByIdAsync(id))
               .ReturnsAsync(contract);

            var response = await _contractService.DeleteContractByIdAsync(id);

            _contractRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
            response.IsSuccess.ShouldBeTrue();
        }

        [Test, AutoData]
        public async Task DeleteContractByIdAsync_ContractNotExist_ReturnSuccess(Guid id)
        {
            _contractRepositoryMock
               .Setup(r => r.GetByIdAsync(id))
               .ReturnsAsync((Contract)null!);

            var response = await _contractService.DeleteContractByIdAsync(id);

            _contractRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Never);
            response.IsSuccess.ShouldBeTrue();
        }

        [Test, AutoData]
        public async Task CreateContractAsync_ContractNotExist_CreateContract(
            Guid id, Guid ownerId, Guid propertyId, Guid? tenantId, string tenantEmail,
            decimal deposit, decimal familyAllowanceFundAmount, DateTimeOffset startDate, DateTimeOffset? endDate, string note)
        {
            // Arrange
            decimal rent = 1000;
            _guidProviderMock
                .Setup(p => p.NewGuid())
                .Returns(id);

            Contract? contractCreated = null;

            _contractRepositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Contract>()))
                .Callback((Contract contract) => { contractCreated = contract; })
                .ReturnsAsync(() => contractCreated!);

            // Act
            await _contractService.CreateContractAsync(ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);

            // Assert
            contractCreated.ShouldNotBeNull();
            contractCreated.Id.ShouldBe(id);
            contractCreated.OwnerId.ShouldBe(ownerId);
            contractCreated.PropertyId.ShouldBe(propertyId);
            contractCreated.TenantId.ShouldBe(tenantId);
            contractCreated.TenantEmail.ShouldBe(tenantEmail);
            contractCreated.Rent.ShouldBe(rent);
            contractCreated.Deposit.ShouldBe(deposit);
            contractCreated.FamilyAllowanceFundAmount.ShouldBe(familyAllowanceFundAmount);
            contractCreated.StartDate.ShouldBe(startDate);
            contractCreated.EndDate.ShouldBe(endDate);
            contractCreated.Note.ShouldBe(note);
        }

        [Test, AutoData]
        public async Task GetContractsByOwnerIdAsync_ContractExist_ReturnContract(Guid ownerId)
        {
            var contract = ContractBuilder
                .Build()
                .WithOwnerId(ownerId)
                .Create();

            var contracts = new List<Contract> { contract };

            _contractRepositoryMock
                .Setup(r => r.GetContractsByOwnerIdAsync(ownerId))
                .ReturnsAsync(contracts);

            var response = await _contractService.GetContractsByOwnerIdAsync(ownerId);

            response.Value.ShouldBe(contracts);
        }

        [Test, AutoData]
        public async Task GetContractsByOwnerIdAsync_ContractNotExist_ReturnFailure(Guid ownerId)
        {
            var contracts = new List<Contract>();

            _contractRepositoryMock
                .Setup(r => r.GetContractsByOwnerIdAsync(ownerId))
                .ReturnsAsync(contracts);

            var response = await _contractService.GetContractsByOwnerIdAsync(ownerId);

            response.IsSuccess.ShouldBeFalse();
            response.Error.ShouldBe(ContractErrors.CouldNotFoundContractsByOwnerId);
        }
    }
}