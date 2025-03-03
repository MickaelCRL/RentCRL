using FluentValidation;
using RentCRL.Domain;

namespace RentCRL.Presentation
{
    public class OwnerModelValidator : AbstractValidator<OwnerModel>
    {
        public OwnerModelValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty();
            RuleFor(o => o.LastName).NotEmpty();
            RuleFor(o => o.Email).NotEmpty().Matches(Regexes.Email);
            RuleFor(o => o.PhoneNumber).NotEmpty().Matches(Regexes.PhoneNumber);

        }
    }
}
