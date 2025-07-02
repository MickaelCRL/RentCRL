using AutoFixture;
using RentCRL.Domain;
using RentCRL.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCRL.Tests.Utils
{
    public class PropertyBuilder
    {
        private readonly Fixture _fixture = new();
        private string _name;
        private decimal _surface;
        private string _status;
        private Address _address;
        private Guid _ownerId;

        private PropertyBuilder()
        {
            _name = _fixture.Create<string>();
            _surface = _fixture.Create<decimal>();
            _status = _fixture.Create<string>();
            _address = _fixture.Create<Address>();
            _ownerId = Guid.NewGuid();
        }
        public static PropertyBuilder Build()
        {
            return new PropertyBuilder();
        }

        public Property Create()
        {
            return new Property(_name, _surface, _status, _address, _ownerId);
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
