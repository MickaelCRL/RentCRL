using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Base;
using RentCRL.Infrastructure.Database;
using Serilog;
using System.Net;

namespace RentCRL.Infrastructure.Base
{
    public abstract class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : Entity
    {
        private readonly CosmosDbSettings _cosmosDbSettings;
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger _logger;

        public EntityRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient, ILogger logger)
        {
            _cosmosDbSettings = cosmosDbSettings;
            _cosmosClient = cosmosClient;
            _logger = logger;
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

        public async Task DeleteAsync(Guid id)
        {
            var stringId = id.ToString();
            try
            {
                var response = await GetContainer().DeleteItemAsync<TEntity>(stringId, new PartitionKey(stringId));
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.Warning("Entity with Id {EntityId} not found for deletion.", id);
            }
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
