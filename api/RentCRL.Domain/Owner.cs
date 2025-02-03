namespace RentCRL.Domain
{
    public class Owner
    {
        Guid Id { get; set; }
        string Auth0Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }

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
