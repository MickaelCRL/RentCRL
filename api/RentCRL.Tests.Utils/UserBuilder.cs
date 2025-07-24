using RentCRL.Domain.Users;

namespace RentCRL.Tests.Utils
{
    public class UserBuilder : UserBuilderGeneric<UserBuilder>
    {
        protected override UserBuilder GetBuilder() => this;

        public static UserBuilder Build()
        {
            return new UserBuilder();
        }

        public override User Create()
        {
            return new User(_id, _auth0Id, _firstName, _lastName, _email, _phoneNumber, _userType);
        }
    }
}
