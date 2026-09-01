using AutoFixture;
using FluentValidation.TestHelper;
using RentCRL.Presentation.Contracts;
using RentCRL.Tests.Utils;

namespace RentCRL.Presentation.Tests.Unit.Contracts
{
    public class ContractModelValidatorTests
    {
        private Fixture _fixture;
        private ContractModelValidator _validator;
        private ContractModel _contractModel;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _validator = new ContractModelValidator();

            var startDate = DateTimeOffset.UtcNow;

            _contractModel = new ContractModel
            (
                _fixture.Create<Guid>(),
                _fixture.Create<Guid>(),
                _fixture.Create<Guid>(),
                _fixture.Create<Guid?>(),
                _fixture.CreateEmail(),
                Math.Abs(_fixture.Create<decimal>()) + 1,
                Math.Abs(_fixture.Create<decimal>()),
                Math.Abs(_fixture.Create<decimal>()),
                startDate,
                startDate.AddMonths(12),
                _fixture.Create<string>()
            );
        }

        [Test]
        public void Validate_ModelIsValid_NoError()
        {
            var contractModel = _contractModel;
            var result = _validator.TestValidate(contractModel);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_PropertyIdIsEmpty_Error()
        {
            var contractModel = _contractModel with { PropertyId = Guid.Empty };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.PropertyId);
        }

        [Test]
        public void Validate_TenantEmailIsEmpty_Error()
        {
            var contractModel = _contractModel with { TenantEmail = string.Empty };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.TenantEmail);
        }

        [Test]
        public void Validate_TenantEmailIsInvalidFormat_Error()
        {
            var contractModel = _contractModel with { TenantEmail = "invalid-email-format" };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.TenantEmail);
        }

        [Test]
        public void Validate_RentIsZeroOrNegative_Error()
        {
            var contractModel = _contractModel with { Rent = 0 };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.Rent);
        }

        [Test]
        public void Validate_DepositIsNegative_Error()
        {
            var contractModel = _contractModel with { Deposit = -1 };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.Deposit);
        }

        [Test]
        public void Validate_FamilyAllowanceFundAmountIsNegative_Error()
        {
            var contractModel = _contractModel with { FamilyAllowanceFundAmount = -1 };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.FamilyAllowanceFundAmount);
        }

        [Test]
        public void Validate_StartDateIsDefault_Error()
        {
            var contractModel = _contractModel with { StartDate = default };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.StartDate);
        }

        [Test]
        public void Validate_EndDateIsBeforeStartDate_Error()
        {
            var startDate = DateTimeOffset.UtcNow;
            var contractModel = _contractModel with
            {
                StartDate = startDate,
                EndDate = startDate.AddDays(-1)
            };
            var result = _validator.TestValidate(contractModel);
            result.ShouldHaveValidationErrorFor(c => c.EndDate);
        }
    }
}