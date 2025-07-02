namespace RentCRL.Domain.Base
{
    public interface IEntity
    {
        Guid Id { get; }
        string EntityType { get; }
        DateTimeOffset? Created { get; }
        DateTimeOffset? Modified { get; }
    }
}
