using RentCRL.Domain.Results;

namespace RentCRL.Domain.Users
{
    public interface IOwnerRepository
    {
        Task<Owner> AddAsync(Owner owner);
        Task<Owner> GetByEmailAsync(string email);
    }
}
