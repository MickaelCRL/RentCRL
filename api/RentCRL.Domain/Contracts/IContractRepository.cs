using RentCRL.Domain.Base;

namespace RentCRL.Domain.Contracts
{
    public interface IContractRepository : IEntityRepository<Contract>
    {
        Task<List<Contract>> GetContractsByOwnerIdAsync(Guid ownerId);
    }
}