using AutoFixture;
using FluentValidation.TestHelper;
using RentCRL.Presentation.Addresses;

namespace RentCRL.Presentation.Tests.Unit.Addresses
{
    public class AddressModelValidatorTests
    {
        private Fixture _fixture;
        private AddressModelValidator _validator;
        private AddressModel _addressModel;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _validator = new();
            _addressModel = new
            (
              _fixture.Create<string>(),
              _fixture.Create<string>(),
              _fixture.Create<string>(),
              _fixture.Create<string>(),
              _fixture.Create<string>()
            );
        }

        [Test]
        public void Validate_ModelIsValid_NoError()
        {
            var result = _validator.TestValidate(_addressModel);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_Line1IsEmpty_Error()
        {
            var addressModel = _addressModel with { Line1 = string.Empty };
            var result = _validator.TestValidate(addressModel);
            result.ShouldHaveValidationErrorFor(a => a.Line1);
        }

        [Test]
        public void Validate_PostalCodeIsEmpty_Error()
        {
            var addressModel = _addressModel with { PostalCode = string.Empty };
            var result = _validator.TestValidate(addressModel);
            result.ShouldHaveValidationErrorFor(a => a.PostalCode);
        }

        [Test]
        public void Validate_CityIsEmpty_Error()
        {
            var addressModel = _addressModel with { City = string.Empty };
            var result = _validator.TestValidate(addressModel);
            result.ShouldHaveValidationErrorFor(a => a.City);
        }

        [Test]
        public void Validate_CountryIsEmpty_Error()
        {
            var addressModel = _addressModel with { Country = string.Empty };
            var result = _validator.TestValidate(addressModel);
            result.ShouldHaveValidationErrorFor(a => a.Country);
        }
    }
}
