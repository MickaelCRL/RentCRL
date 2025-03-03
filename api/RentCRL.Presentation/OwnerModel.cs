namespace RentCRL.Presentation
{
    public record OwnerModel
    (
        Guid Id,
        string Auth0Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber
    )
    {}  
    
}
