
namespace RentCRL.Domain.Users
{
    public class Tenant : User
    {
        public Tenant(Guid id, string auth0Id, string firstName, string lastName, string email, string phoneNumber) 
            : base(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(Tenant))
        { }
    }
}
