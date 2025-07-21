using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Properties;
using RentCRL.Infrastructure.Base;

namespace RentCRL.Infrastructure.Properties
{
    public class PropertyRepository : EntityRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(CosmosDbService cosmosDbService) 
            : base(cosmosDbService)
        { }

        public async Task<List<Property>> GetPropertiesByOwnerIdAsync(Guid ownerId)
        {
            var feedIterator = _container.GetItemLinqQueryable<Property>(
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
