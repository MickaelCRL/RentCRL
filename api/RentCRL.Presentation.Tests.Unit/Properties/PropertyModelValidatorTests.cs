using AutoFixture;
using FluentValidation.TestHelper;
using RentCRL.Presentation.Addresses;
using RentCRL.Presentation.Properties;

namespace RentCRL.Presentation.Tests.Unit.Properties
{
    public class PropertyModelValidatorTests
    {
        private Fixture _fixture;
        private PropertyModelValidator _validator;
        private PropertyModel _propertyModel;

        [SetUp]
        public void Setup()
        {
            _fixture = new();
            _validator = new();
            _propertyModel = new
            (
                _fixture.Create<Guid>(),
                _fixture.Create<string>(),
                _fixture.Create<decimal>(),
                _fixture.Create<string>(),
                _fixture.Create<AddressModel>(),
                _fixture.Create<Guid>()
            );
        }

        [Test]
        public void Validate_ModelIsValid_NoError()
        {
            var propertyModel = _propertyModel;
            var result = _validator.TestValidate(propertyModel);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void Validate_NameIsEmpty_Error()
        {
            var propertyModel = _propertyModel with { Name = string.Empty };
            var result = _validator.TestValidate(propertyModel);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }

        [Test]
        public void Validate_SurfaceIsNegative_Error()
        {
            var propertyModel = _propertyModel with { Surface = -1 };
            var result = _validator.TestValidate(propertyModel);
            result.ShouldHaveValidationErrorFor(p => p.Surface);
        }

        [Test]
        public void Validate_Address_UsesAddressModelValidator()
        {
            _validator.ShouldHaveChildValidator(p => p.Address, typeof(AddressModelValidator));
        }
    }
}
