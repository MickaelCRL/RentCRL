using RentCRL.Domain.Results;

namespace RentCRL.Domain.Users
{
    public class UserErrors
    {
        public static readonly Error EmailAlreadyExists = new("UserWithEmailAlreadyExists", "User with email already exists.");
    }
}
