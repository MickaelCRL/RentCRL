using Newtonsoft.Json;

namespace RentCRL.Domain.Base
{
    public abstract class Entity : IEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; private set; }

        public string EntityType { get; }

        public DateTimeOffset? Created { get; protected set; }

        public DateTimeOffset? Modified { get; protected set; }

        protected Entity(
            Guid id,
            string entityType
        )
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Id cannot be empty", nameof(id));

            if (string.IsNullOrEmpty(entityType))
                throw new ArgumentException("EntityType cannot be null or empty", nameof(entityType));

            Id = id;
            EntityType = entityType;
            Created = DateTimeOffset.UtcNow;
        }

        // Constructor for database
        protected Entity(Guid id, string entityType, DateTimeOffset? created, DateTimeOffset? modified) 
            : this(id, entityType)
        {
            Created = created;
            Modified = modified;
        }
    }
}
