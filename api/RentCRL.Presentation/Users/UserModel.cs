namespace RentCRL.Presentation.Users
{
    public record UserModel
      (
        Guid Id,
        string Auth0Id,
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber,
        string EntityType
    )
    { }
}
