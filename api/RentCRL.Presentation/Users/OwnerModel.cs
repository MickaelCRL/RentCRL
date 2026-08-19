using RentCRL.Domain.Users;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Users
{
    public record OwnerModel : UserModel
    {
        public AddressModel Address { get; init; }

        public OwnerModel(
            Guid Id,
            string Auth0Id,
            string FirstName,
            string LastName,
            string Email,
            string PhoneNumber,
            AddressModel Address
        )
        : base(Id, Auth0Id, FirstName, LastName, Email, PhoneNumber, nameof(Owner)) 
        {
            this.Address = Address;
        }
    }
}
