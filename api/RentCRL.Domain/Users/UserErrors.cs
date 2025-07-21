using RentCRL.Domain.Results;

namespace RentCRL.Domain.Users
{
    public class UserErrors
    {
        public static readonly Error EmailAlreadyExists = new("UserWithEmailAlreadyExists", "User with email already exists.");
        public static readonly Error CouldNotFindUserWithEmail = new("CouldNotFindUserWithEmail", "Could not find user by email.");
        public static readonly Error CouldNotFindUserWithId = new("CouldNotFindUserWithId", "Could not find user by id.");
    }
}
