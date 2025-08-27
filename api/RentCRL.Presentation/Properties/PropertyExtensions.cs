using RentCRL.Domain.Properties;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Properties
{
    public static class PropertyExtensions
    {
        public static PropertyModel ToModel(this Property property)
        {
            return new PropertyModel(
                property.Id,
                property.Name,
                property.Surface,
                property.Status,
                property.Address.ToModel(),
                property.OwnerId
            );
        }
    }
}
