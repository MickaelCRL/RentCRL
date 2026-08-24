using AutoFixture.NUnit3;
using RentCRL.Domain.Contracts;
using RentCRL.Infrastructure.Contracts;
using RentCRL.Tests.Utils;
using Serilog;
using Shouldly;

namespace RentCRL.Infrastructure.Tests.Integration.Contracts
{
    public class ContractRepositoryTests : BaseTests
    {
        private ContractRepository _contractRepository;

        [SetUp]
        public async Task Setup()
        {
            await BaseSetUp();
            var silentLogger = new LoggerConfiguration().CreateLogger();
            _contractRepository = new ContractRepository(_cosmosDbTestEnvironment.Settings, _cosmosDbTestEnvironment.Client, silentLogger);
        }

        [Test, AutoData]
        public async Task GetContractsByOwnerIdAsync_ContractExists_ReturnsContracts(Guid ownerId)
        {
            var unrelatedContract1 = ContractBuilder.Build().Create();
            var unrelatedContract2 = ContractBuilder.Build().Create();

            var expectedContract1 = ContractBuilder.Build()
                .WithOwnerId(ownerId)
                .Create();

            var expectedContract2 = ContractBuilder.Build()
                .WithOwnerId(ownerId)
                .Create();

            var expectedResult = new List<Contract> { expectedContract1, expectedContract2 };

            var container = _cosmosDbTestEnvironment.GetEntitiesContainer();

            await container.CreateItemAsync(unrelatedContract1);
            await container.CreateItemAsync(unrelatedContract2);
            await container.CreateItemAsync(expectedContract1);
            await container.CreateItemAsync(expectedContract2);

            var result = await _contractRepository.GetContractsByOwnerIdAsync(ownerId);

            result.ShouldBeEquivalentTo(expectedResult);
        }

        [Test, AutoData]
        public async Task GetContractsByOwnerIdAsync_ContractNotExist_ReturnsListEmpty(Guid ownerId)
        {
            var result = await _contractRepository.GetContractsByOwnerIdAsync(ownerId);

            result.ShouldBeEmpty();
        }
    }
}