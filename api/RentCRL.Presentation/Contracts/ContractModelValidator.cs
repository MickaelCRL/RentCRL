using FluentValidation;

namespace RentCRL.Presentation.Contracts
{
    public class ContractModelValidator : AbstractValidator<ContractModel>
    {
        public ContractModelValidator()
        {
            RuleFor(c => c.PropertyId).NotEmpty();
            RuleFor(c => c.TenantEmail).NotEmpty().EmailAddress();
            RuleFor(c => c.Rent).GreaterThan(0);
            RuleFor(c => c.Deposit).GreaterThanOrEqualTo(0);
            RuleFor(c => c.FamilyAllowanceFundAmount).GreaterThanOrEqualTo(0);
            RuleFor(c => c.StartDate).NotEmpty();
            RuleFor(c => c.EndDate)
                .GreaterThan(c => c.StartDate)
                .When(c => c.EndDate.HasValue);
        }
    }
}