using FluentValidation;
using RentCRL.Domain;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Users
{
    public class OwnerModelValidator : AbstractValidator<OwnerModel>
    {
        public OwnerModelValidator()
        {
            RuleFor(o => o.Auth0Id).NotEmpty();
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.Email).NotEmpty().Matches(Regexes.Email);
            RuleFor(o => o.PhoneNumber).NotEmpty().Matches(Regexes.PhoneNumber);
            RuleFor(o => o.Address).NotNull().SetValidator(new AddressModelValidator());
        }
    }
}
