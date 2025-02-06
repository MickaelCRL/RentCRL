using RentCRL.Domain;

namespace RentCRL.Application
{
    public class OwnerService : IOwnerService
    {
        private IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<Owner> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber)
        {
            var newOwner = new Owner(auth0Id, firstName, lastName, email, phoneNumber);

            return await _ownerRepository.AddAsync(newOwner);
            
        }       
    }
}
