using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain;
using RentCRL.Domain.Properties;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;
using Serilog;

namespace RentCRL.Infrastructure.Properties
{
    public class PropertyRepository : EntityRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient, ILogger logger)
            : base(cosmosDbSettings, cosmosClient, logger)
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
            
            var results = new List<Property>();

            while (feedIterator.HasMoreResults)
            {
                var response = await feedIterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<Property> UpdatePropertyAsync(Guid propertyId, string name, decimal surface, string status, Address address)
        {
            var stringId = propertyId.ToString();
            var patchOperations = new List<PatchOperation>
            {
                PatchOperation.Replace("/Name", name),
                PatchOperation.Replace("/Surface", surface),
                PatchOperation.Replace("/Status", status),
                PatchOperation.Replace("/Address", address),
            };

            var response = await GetContainer().PatchItemAsync<Property>(
                id: stringId,
                partitionKey: new PartitionKey(stringId),
                patchOperations
            );

            return response;
        }
    }
}
