using Newtonsoft.Json;
using RentCRL.Domain.Base;
namespace RentCRL.Domain.Users
{
    public class User : Entity
    {
        public string Auth0Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string PhoneNumber { get; protected set; }

        public User(Guid id, string auth0Id, string firstName, string lastName, string email, string phoneNumber, string entityType)
            : base(id, entityType)
        {
            if (string.IsNullOrEmpty(auth0Id))
                throw new ArgumentException("Auth0Id cannot be null.", nameof(auth0Id));

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("Firstname cannot be null.", nameof(firstName));

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("LastName cannot be null.", nameof(lastName));

            if (!Regexes.Email.IsMatch(email))
                throw new ArgumentException("Email is not valid.", nameof(email));

            if (!Regexes.PhoneNumber.IsMatch(phoneNumber))
                throw new ArgumentException("PhoneNumber is not valid.", nameof(phoneNumber));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("EntityType cannot be null or empty", nameof(entityType));

            Auth0Id = auth0Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
