using RentCRL.Domain.Users;

namespace RentCRL.Presentation.Users
{
    public static class UserExtensions
    {
        public static UserModel ToModel(this User user)
        {
            return new UserModel(
                user.Id,
                user.Auth0Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.PhoneNumber,
                user.EntityType
            );
        }
    }
}
