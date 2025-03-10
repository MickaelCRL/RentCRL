using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

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
