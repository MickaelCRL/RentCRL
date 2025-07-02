using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Azure.Documents;
using RentCRL.Domain.Base;
using RentCRL.Domain.Users;

namespace RentCRL.Infrastructure.Base
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity
    {
        private readonly CosmosDbService _cosmosDbService;
        protected readonly Container _container;

        public EntityRepository(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
            _container = _cosmosDbService.GetContainer("Entities").Result;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var response = await _container.CreateItemAsync(entity);
            return response.Resource;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var feedIterator = _container.GetItemLinqQueryable<TEntity>(
                             requestOptions: new QueryRequestOptions
                             {
                                 PartitionKey = null
                             })
                             .Where(e => e.Id == id)
                             .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.SingleOrDefault();
        }
    }
}
