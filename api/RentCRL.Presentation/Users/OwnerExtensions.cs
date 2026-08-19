using RentCRL.Domain;
using RentCRL.Domain.Users;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Users
{
    public static class OwnerExtensions
    {
        public static OwnerModel ToModel(this Owner owner)
        {
            return new OwnerModel(
                owner.Id,
                owner.Auth0Id,
                owner.FirstName,
                owner.LastName,
                owner.Email,
                owner.PhoneNumber,
                owner.Address.ToModel()
            );
        }
    }
}
