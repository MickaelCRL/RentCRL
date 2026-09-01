using RentCRL.Domain.Contracts;
using RentCRL.Domain.Results;

namespace RentCRL.Application.Contracts
{
    public interface IContractService
    {
        Task<Result<Contract>> CreateContractAsync(Guid ownerId, Guid propertyId, Guid? tenantId, string tenantEmail, decimal rent, decimal deposit, decimal familyAllowanceFundAmount, DateTimeOffset startDate, DateTimeOffset? endDate, string? note);
        Task<Result<Contract>> GetContractByIdAsync(Guid contractId);
        Task<Result> DeleteContractByIdAsync(Guid contractId);
        Task<Result<List<Contract>>> GetContractsByOwnerIdAsync(Guid ownerId);
    }
}