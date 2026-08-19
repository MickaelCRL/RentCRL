using RentCRL.Domain;
using RentCRL.Domain.Results;
using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public interface IOwnerService
    {
        Task<Result<Owner>> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber, Address address);

        Task<Result<Owner>> GetOwnerByIdAsync(Guid ownerId);
    }
}
