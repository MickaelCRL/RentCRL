using AutoFixture;
using FluentValidation.TestHelper;
using RentCRL.Presentation.Users;
using RentCRL.Tests.Utils;

namespace RentCRL.Presentation.Tests.Unit.Users
{
    public class OwnerModelValidatorTests
    {
        private Fixture _fixture;
        private OwnerModelValidator _validator;
        private OwnerModel _ownerModel;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _validator = new();
            _ownerModel = new
            (
                _fixture.Create<Guid>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.CreateEmail(),
                _fixture.CreatePhoneNumber()
            );
        }

        [Test]
        public void Validate_ModelIsValid_NoError()
        {
            var ownerModel = _ownerModel;
            var result = _validator.TestValidate(ownerModel);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_FirstNameIsEmpty_Error()
        {
            var ownerModel = _ownerModel with { FirstName = string.Empty };
            var result = _validator.TestValidate(ownerModel);
            result.ShouldHaveValidationErrorFor(o => o.FirstName);
        }

        [Test]
        public void Validate_LastNameIsEmpty_Error()
        {
            var ownerModel = _ownerModel with { LastName = string.Empty };
            var result = _validator.TestValidate(ownerModel);
            result.ShouldHaveValidationErrorFor(o => o.LastName);
        }

        [Test]
        public void Validate_EmailNotMatchRegex_Error()
        {
            var ownerModel = _ownerModel with { Email = string.Empty };
            var result = _validator.TestValidate(ownerModel);
            result.ShouldHaveValidationErrorFor(o => o.Email);
        }

        [Test]
        public void Validate_PhoneNumberNotMatchRegex_Error()
        {
            var ownerModel = _ownerModel with { PhoneNumber = string.Empty };
            var result = _validator.TestValidate(ownerModel);
            result.ShouldHaveValidationErrorFor(o => o.PhoneNumber);
        }
    }
}
