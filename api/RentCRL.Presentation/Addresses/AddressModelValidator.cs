using FluentValidation;

namespace RentCRL.Presentation.Addresses
{
    public class AddressModelValidator : AbstractValidator<AddressModel>
    {
        public AddressModelValidator()
        {
            RuleFor(a => a.Line1).NotEmpty();
            RuleFor(a => a.PostalCode).NotEmpty();
            RuleFor(a => a.City).NotEmpty();
            RuleFor(a => a.Country).NotEmpty();
        }
    }
}
