using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Properties
{
    public record PropertyModel
    (
        Guid Id,
        string Name,
        decimal Surface,
        string Status,
        AddressModel Address,
        Guid OwnerId
    )
    { }
}
