using AutoFixture;
using RentCRL.Domain.Contracts;

namespace RentCRL.Tests.Utils
{
    public class ContractBuilder
    {
        private readonly Fixture _fixture = new();
        private Guid _id;
        private Guid _ownerId;
        private Guid _propertyId;
        private Guid? _tenantId;
        private string _tenantEmail;
        private decimal _rent;
        private decimal _deposit;
        private decimal _familyAllowanceFundAmount;
        private DateTimeOffset _startDate;
        private DateTimeOffset? _endDate;
        private string _note;

        public ContractBuilder()
        {
            _id = _fixture.Create<Guid>();
            _ownerId = _fixture.Create<Guid>();
            _propertyId = _fixture.Create<Guid>();
            _tenantId = _fixture.Create<Guid?>();
            _tenantEmail = _fixture.Create<string>() + "@example.com";
            _rent = Math.Abs(_fixture.Create<decimal>()) + 1;
            _deposit = Math.Abs(_fixture.Create<decimal>());
            _familyAllowanceFundAmount = Math.Abs(_fixture.Create<decimal>());
            _startDate = DateTimeOffset.UtcNow;
            _endDate = _startDate.AddYears(1);
            _note = _fixture.Create<string>();
        }

        public static ContractBuilder Build() => new();

        public ContractBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public ContractBuilder WithOwnerId(Guid ownerId)
        {
            _ownerId = ownerId;
            return this;
        }

        public Contract Create()
        {
            return new Contract(
                _id,
                _ownerId,
                _propertyId,
                _tenantId,
                _tenantEmail,
                _rent,
                _deposit,
                _familyAllowanceFundAmount,
                _startDate,
                _endDate,
                _note
            );
        }
    }
}