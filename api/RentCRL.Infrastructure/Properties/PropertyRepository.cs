using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Properties;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;

namespace RentCRL.Infrastructure.Properties
{
    public class PropertyRepository : EntityRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient) 
            : base(cosmosDbSettings, cosmosClient)
        { }

        public async Task<List<Property>> GetPropertiesByOwnerIdAsync(Guid ownerId)
        {
            var feedIterator = GetContainer().GetItemLinqQueryable<Property>(
                              requestOptions: new QueryRequestOptions
                              {
                                  PartitionKey = null
                              })
                              .Where(p => p.OwnerId == ownerId && p.EntityType == nameof(Property))
                              .ToFeedIterator();
            var response = await feedIterator.ReadNextAsync();
            return response.ToList();
        }
    }
}
