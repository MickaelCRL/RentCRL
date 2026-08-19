using RentCRL.Domain;
using RentCRL.Domain.Users;

namespace RentCRL.Tests.Utils
{
    public class OwnerBuilder : UserBuilderGeneric<OwnerBuilder>
    {
        private Address _address = new Address("123 rue par défaut", null, "75000", "Paris", "France");

        protected override OwnerBuilder GetBuilder() => this;

        public static OwnerBuilder Build()
        {
            return new OwnerBuilder();
        }

        public OwnerBuilder WithAddress(Address address)
        {
            _address = address;
            return this;
        }

        public override Owner Create()
        {
            return new Owner(_id, _auth0Id, _firstName, _lastName, _email, _phoneNumber, _address);
        }
    }
}
