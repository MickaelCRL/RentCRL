using RentCRL.Domain;
using RentCRL.Domain.Users;

namespace RentCRL.Infrastructure.Users
{
    public class OwnerRepository : IOwnerRepository
    {
        public async Task<Owner> AddAsync(Owner owner)
        {
            return await Task.FromResult(owner);
        }
    }
}
