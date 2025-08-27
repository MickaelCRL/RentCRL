using RentCRL.Domain.Users;

namespace RentCRL.Tests.Utils
{
    public class OwnerBuilder : UserBuilderGeneric<OwnerBuilder>
    {
        protected override OwnerBuilder GetBuilder() => this;

        public static OwnerBuilder Build()
        {
            return new OwnerBuilder();
        }

        public override Owner Create()
        {
            return new Owner(_id, _auth0Id, _firstName, _lastName, _email, _phoneNumber);
        }
    }
}
