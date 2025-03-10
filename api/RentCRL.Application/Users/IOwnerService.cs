using RentCRL.Domain.Users;

namespace RentCRL.Application.Users
{
    public interface IOwnerService
    {
        Task<Owner> CreateOwnerAsync(string auth0Id, string firstName, string lastName, string email, string phoneNumber);
    }
}
