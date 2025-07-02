using RentCRL.Domain.Users;

namespace RentCRL.Presentation.Users
{
    public record OwnerModel : UserModel
    {
        public OwnerModel(
            Guid Id,
            string Auth0Id,
            string FirstName,
            string LastName,
            string Email,
            string PhoneNumber
        )
        : base(Id, Auth0Id, FirstName, LastName, Email, PhoneNumber, nameof(Owner)) { }
    }
}
