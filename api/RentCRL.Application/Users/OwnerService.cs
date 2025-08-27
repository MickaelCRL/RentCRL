using RentCRL.Domain.Base;
using RentCRL.Domain.Results;
using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public class OwnerService : IOwnerService
    {
        private readonly IGuidProvider _guidProvider;
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IGuidProvider guidProvider, IOwnerRepository ownerRepository)
        {
            _guidProvider = guidProvider;
            _ownerRepository = ownerRepository;
        }

        public async Task<Result<Owner>> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber)
        {
            var owner = await _ownerRepository.GetByEmailAsync(email);
            if (owner != null)
                return UserErrors.EmailAlreadyExists;

            var newOwner = new Owner(_guidProvider.NewGuid(), auth0Id, firstName, lastName, email, phoneNumber);
            var response = await _ownerRepository.AddAsync(newOwner);

            return response;
        }

        public async Task<Result<Owner>> GetOwnerByIdAsync(Guid ownerId)
        {
            var owner = await _ownerRepository.GetByIdAsync(ownerId);
            if (owner == null)
                return UserErrors.CouldNotFindUserWithId;

            return owner;
        }
    }
}
