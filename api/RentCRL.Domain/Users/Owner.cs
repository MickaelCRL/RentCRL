using Newtonsoft.Json;

namespace RentCRL.Domain.Users
{
    public class Owner : User
    {
        public Address Address { get; private set; }

        public Owner(Guid id, string auth0Id, string firstName, string lastName, string email, string phoneNumber, Address address)
            : base(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(Owner))
        {
            Address = address;
        }

        [JsonConstructor]
        public Owner
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            Address address,
            DateTimeOffset? created,
            DateTimeOffset? modified
        )
          : base(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(Owner), created, modified)
        {
            Address = address;
        }
    }
}
