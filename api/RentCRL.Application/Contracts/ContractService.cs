using RentCRL.Domain.Base;
using RentCRL.Domain.Contracts;
using RentCRL.Domain.Results;

namespace RentCRL.Application.Contracts
{
    public class ContractService : IContractService
    {
        private readonly IGuidProvider _guidProvider;
        private readonly IContractRepository _contractRepository;

        public ContractService(IGuidProvider guidProvider, IContractRepository contractRepository)
        {
            _guidProvider = guidProvider;
            _contractRepository = contractRepository;
        }

        public async Task<Result<Contract>> CreateContractAsync(Guid ownerId, Guid propertyId, Guid? tenantId, string tenantEmail, decimal rent, decimal deposit, decimal familyAllowanceFundAmount, DateTimeOffset startDate, DateTimeOffset? endDate, string? note)
        {
            var contract = new Contract(_guidProvider.NewGuid(), ownerId, propertyId, tenantId, tenantEmail, rent, deposit, familyAllowanceFundAmount, startDate, endDate, note);
            return await _contractRepository.AddAsync(contract);
        }

        public async Task<Result<Contract>> GetContractByIdAsync(Guid contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);
            if (contract == null)
                return ContractErrors.CouldNotFoundContractById;

            return contract;
        }

        public async Task<Result> DeleteContractByIdAsync(Guid contractId)
        {
            var contract = await _contractRepository.GetByIdAsync(contractId);

            if (contract != null)
                await _contractRepository.DeleteAsync(contract.Id);

            return Result.Success();
        }

        public async Task<Result<List<Contract>>> GetContractsByOwnerIdAsync(Guid ownerId)
        {
            var response = await _contractRepository.GetContractsByOwnerIdAsync(ownerId);

            if (response == null || response.Count == 0)
                return ContractErrors.CouldNotFoundContractsByOwnerId;

            return response;
        }
    }
}