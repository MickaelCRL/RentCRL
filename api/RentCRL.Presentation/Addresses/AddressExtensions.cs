using RentCRL.Domain;

namespace RentCRL.Presentation.Addresses
{
    public static class AddressExtensions
    {
        public static AddressModel ToModel(this Address address)
        {
            var addressModel = new AddressModel(
                address.Line1,
                address.Line2,
                address.PostalCode,
                address.City,
                address.Country
            );

            return addressModel;
        }
    }
}
