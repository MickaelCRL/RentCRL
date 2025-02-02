namespace RentCRL.Domain
{
    public class Owner
    {
        public Guid Id { get; set; }
        public string Auth0Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Owner(string auth0Id, string firstName, string lastName, string email, string phoneNumber)
        {
            Id = Guid.NewGuid();
            Console.WriteLine($"The new Id: {Id}");
            Auth0Id = auth0Id;
            FirstName = firstName;
            Console.WriteLine($"The FirstName: {FirstName}");
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
        }



    }
}
