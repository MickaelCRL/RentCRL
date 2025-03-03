using System.Net.Mail;

namespace RentCRL.Domain
{
    public class Owner
    {
        public Guid Id { get; private set; }
        public string Auth0Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public Owner(string auth0Id, string firstName, string lastName, string email, string phoneNumber)
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

            Id = Guid.NewGuid();
            Auth0Id = auth0Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
