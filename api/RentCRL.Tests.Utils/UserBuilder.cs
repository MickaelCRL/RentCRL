using AutoFixture;
using RentCRL.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCRL.Tests.Utils
{
    public class UserBuilder
    {
        private readonly Fixture _fixture = new();
        private Guid _id;
        private string _auth0Id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _userType;

        private UserBuilder()
        {
            _id = _fixture.Create<Guid>();
            _auth0Id = _fixture.Create<string>();
            _firstName = _fixture.Create<string>();
            _lastName = _fixture.Create<string>();
            _email = _fixture.CreateEmail();
            _phoneNumber = _fixture.CreatePhoneNumber();
            _userType = _fixture.Create<string>();
        }
        public static UserBuilder Build()
        {
            return new UserBuilder();
        }

        public User Create()
        {
            return new User(_id, _auth0Id, _firstName, _lastName, _email, _phoneNumber, _userType);
        }
        public UserBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public UserBuilder WithAuth0Id(string auth0Id)
        {
            _auth0Id = auth0Id;
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        }

        public UserBuilder WithUsertype(string userType)
        {
            _userType = userType;
            return this;
        }
    }
}
