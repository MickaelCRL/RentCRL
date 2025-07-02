using RentCRL.Domain.Base;

namespace RentCRL.Domain.Users
{
    public interface IOwnerRepository : IEntityRepository<Owner>
    { 
        Task<Owner> GetByEmailAsync(string email);
    }
}
