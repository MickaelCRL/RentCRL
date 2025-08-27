namespace RentCRL.Domain.Base
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
