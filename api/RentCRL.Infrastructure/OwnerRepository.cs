using RentCRL.Domain;

namespace RentCRL.Infrastructure
{
    public class OwnerRepository : IOwnerRepository
    {
        public async Task<Owner> AddAsync(Owner owner)
        {
            return await Task.FromResult(owner);
           
        }
    }
}
