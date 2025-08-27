using FluentValidation;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Properties
{
    public class PropertyModelValidator : AbstractValidator<PropertyModel>
    {
        public PropertyModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Surface).GreaterThan(0);
            RuleFor(p => p.Address).SetValidator(new AddressModelValidator());
        }
    }
}
