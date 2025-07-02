using RentCRL.Domain;

namespace RentCRL.Presentation.Addresses
{
    public static class AddressModelExtensions
    {
        public static Address ToAddress(this AddressModel addressModel)
        {
            var address = new Address(
                addressModel.Line1,
                addressModel.Line2,
                addressModel.PostalCode,
                addressModel.City,
                addressModel.Country
            );
            
            return address;
        }
    }
}
