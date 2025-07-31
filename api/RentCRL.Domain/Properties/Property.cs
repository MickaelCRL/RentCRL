using Newtonsoft.Json;
using RentCRL.Domain.Base;

namespace RentCRL.Domain.Properties
{
    public class Property : Entity
    {
        public string Name { get; private set; }
        public decimal Surface { get; private set; }
        public string Status { get; private set; }
        public Address Address { get; private set; }
        public Guid OwnerId { get; private set; }


        public Property(Guid id, string name, decimal surface, string status, Address address, Guid ownerId)
            : base(id, nameof(Property))
        {
            InitializeProperties(name, surface, status, address, ownerId);
        }

        [JsonConstructor]
        public Property
        (
            Guid id,
            string name,
            decimal surface,
            string status,
            Address address,
            Guid ownerId,
            DateTimeOffset? created,
            DateTimeOffset? modified
        ) : base(id, nameof(Property), created, modified)
        {
            InitializeProperties(name, surface, status, address, ownerId);
        }

        private void InitializeProperties(string name, decimal surface, string status, Address address, Guid ownerId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be null.", nameof(name));

            if (surface <= 0)
                throw new ArgumentException("Surface must be greater than zero.", nameof(surface));

            if (ownerId == Guid.Empty)
                throw new ArgumentException("OwnerId cannot be empty.", nameof(ownerId));

            Name = name;
            Surface = surface;
            Status = status;
            Address = address;
            OwnerId = ownerId;
        }
    }
}
