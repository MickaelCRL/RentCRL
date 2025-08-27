using Newtonsoft.Json;

namespace RentCRL.Domain.Users
{
    public class Owner : User
    {
        public Owner(Guid id, string auth0Id, string firstName, string lastName, string email, string phoneNumber)
            : base(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(Owner))
        { }

        [JsonConstructor]
        public Owner
        (
            Guid id,
            string auth0Id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            DateTimeOffset? created,
            DateTimeOffset? modified
        )
          : base(id, auth0Id, firstName, lastName, email, phoneNumber, nameof(Owner), created, modified)
        { }
    }
}
