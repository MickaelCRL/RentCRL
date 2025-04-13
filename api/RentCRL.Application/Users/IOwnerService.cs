using RentCRL.Domain.Users;
using RentCRL.Domain.Results;

namespace RentCRL.Application.Users
{
    public interface IOwnerService
    {
        Task<Result<Owner>> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber);
    }
}
