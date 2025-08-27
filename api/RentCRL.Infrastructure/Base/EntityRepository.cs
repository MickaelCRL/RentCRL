using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Base;
using RentCRL.Infrastructure.Database;

namespace RentCRL.Infrastructure.Base
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity
    {
        private readonly CosmosDbSettings _cosmosDbSettings;
        private readonly CosmosClient _cosmosClient;

        public EntityRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient)
        {
            _cosmosDbSettings = cosmosDbSettings;
            _cosmosClient = cosmosClient;
        }

        protected Container GetContainer()
        {
            return _cosmosClient.GetContainer(_cosmosDbSettings.DatabaseId, ContainersNames.Entities);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var response = await GetContainer().CreateItemAsync(entity);
            return response.Resource;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            var feedIterator = GetContainer().GetItemLinqQueryable<TEntity>(
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
