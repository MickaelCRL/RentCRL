using RentCRL.Domain.Results;
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

        public async Task<Result<Owner>> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber)
        {
            var response = _ownerRepository.GetByEmailAsync(email);
            if (response.Result != null)
                return Result.Failure<Owner>(UserErrors.EmailAlreadyExists);

            Guid id = Guid.NewGuid();
            var newOwner = new Owner(id, auth0Id, firstName, lastName, email, phoneNumber);
            return await _ownerRepository.AddAsync(newOwner);
        }

        public async Task<Result<Owner>> GetOwnerByIdAsync(Guid ownerId)
        {
            var response = _ownerRepository.GetByIdAsync(ownerId);
            if (response.Result == null)
                return Result.Failure<Owner>("Owner not found");

            return await response;
        }
    }
}
