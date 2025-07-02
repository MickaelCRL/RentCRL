namespace RentCRL.Presentation.Addresses
{
    public record AddressModel(
        string Line1,
        string Line2,
        string PostalCode,
        string City,
        string Country
    )
    { }
}
