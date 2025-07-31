using AutoFixture;
using RentCRL.Domain.Users;

namespace RentCRL.Tests.Utils
{
    public abstract class UserBuilderGeneric<TBuilder> where TBuilder : UserBuilderGeneric<TBuilder>
    {
        protected readonly Fixture _fixture = new();
        protected Guid _id;
        protected string _auth0Id;
        protected string _firstName;
        protected string _lastName;
        protected string _email;
        protected string _phoneNumber;
        protected string _userType;

        protected UserBuilderGeneric()
        {
            _id = _fixture.Create<Guid>();
            _auth0Id = _fixture.Create<string>();
            _firstName = _fixture.Create<string>();
            _lastName = _fixture.Create<string>();
            _email = _fixture.CreateEmail();
            _phoneNumber = _fixture.CreatePhoneNumber();
            _userType = nameof(Owner);
        }

        protected abstract TBuilder GetBuilder();

        public abstract User Create();

        public TBuilder WithId(Guid id)
        {
            _id = id;
            return GetBuilder();
        }

        public TBuilder WithAuth0Id(string auth0Id)
        {
            _auth0Id = auth0Id;
            return GetBuilder();
        }

        public TBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return GetBuilder();
        }

        public TBuilder WithLastName(string lastName) 
        { 
            _lastName = lastName;
            return GetBuilder();
        }

        public TBuilder WithEmail(string email) 
        { 
            _email = email;
            return GetBuilder();
        }

        public TBuilder WithPhoneNumber(string phoneNumber) { 
            _phoneNumber = phoneNumber; 
            return GetBuilder(); 
        }

        public TBuilder WithUsertype(string userType) 
        {
            _userType = userType;
            return GetBuilder();
        }
    }
}