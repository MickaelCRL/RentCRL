using AutoFixture;
using RentCRL.Domain.Users;

namespace RentCRL.Tests.Utils
{
    public class OwnerBuilder
    {
        private readonly Fixture _fixture = new();
        private string _auth0Id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;

        private OwnerBuilder()
        {
            _auth0Id = _fixture.Create<string>();
            _firstName = _fixture.Create<string>();
            _lastName = _fixture.Create<string>();
            _email = _fixture.CreateEmail();
            _phoneNumber = _fixture.CreatePhoneNumber();
        }
        public static OwnerBuilder Build()
        {
            return new OwnerBuilder();
        }

        public Owner Create()
        {
            return new Owner(_auth0Id, _firstName, _lastName, _email, _phoneNumber);
        }

        public OwnerBuilder WithAuth0Id(string auth0Id)
        {
            _auth0Id = auth0Id;
            return this;
        }

        public OwnerBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public OwnerBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public OwnerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public OwnerBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }
    }
}
