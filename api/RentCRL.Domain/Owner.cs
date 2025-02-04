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
            Id = Guid.NewGuid();
            Auth0Id = auth0Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }
}
