using Newtonsoft.Json;

namespace RentCRL.Domain.Base
{
    public abstract class Entity : IEntity
    {
        [JsonProperty("id")]
        public Guid Id { get; private set; }
        public string EntityType { get; }

        [JsonProperty("created")]
        public DateTimeOffset? Created { get; protected set; }

        [JsonProperty("modified")]
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
    }
}
