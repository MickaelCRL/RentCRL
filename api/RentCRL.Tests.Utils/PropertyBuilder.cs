using AutoFixture;
using RentCRL.Domain;
using RentCRL.Domain.Properties;

namespace RentCRL.Tests.Utils
{
    public class PropertyBuilder
    {
        private readonly Fixture _fixture = new();
        private Guid _id;
        private string _name;
        private decimal _surface;
        private string _status;
        private Address _address;
        private Guid _ownerId;

        private PropertyBuilder()
        {
            _id = _fixture.Create<Guid>();
            _name = _fixture.Create<string>();
            _surface = _fixture.Create<decimal>();
            _status = _fixture.Create<string>();
            _address = _fixture.Create<Address>();
            _ownerId = _fixture.Create<Guid>();
        }
        public static PropertyBuilder Build()
        {
            return new PropertyBuilder();
        }

        public Property Create()
        {
            return new Property(_id, _name, _surface, _status, _address, _ownerId);
        }

        public PropertyBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PropertyBuilder WithSurface(decimal surface)
        {
            _surface = surface;
            return this;
        }

        public PropertyBuilder WithStatus(string status)
        {
            _status = status;
            return this;
        }

        public PropertyBuilder WithOwnerId(Guid ownerId)
        {
            _ownerId = ownerId;
            return this;
        }
    }
}
