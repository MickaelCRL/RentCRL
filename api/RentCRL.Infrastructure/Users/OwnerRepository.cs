using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;
using Serilog;

namespace RentCRL.Infrastructure.Users
{
    public class OwnerRepository : EntityRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient, ILogger logger)
            : base(cosmosDbSettings, cosmosClient, logger)
        { }

        public async Task<Owner> GetByEmailAsync(string email)
        {
            var feedIterator = GetContainer().GetItemLinqQueryable<Owner>(
                              requestOptions: new QueryRequestOptions
                              {
                                  PartitionKey = null
                              })
                              .Where(u => u.Email == email && u.EntityType == nameof(Owner))
                              .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.SingleOrDefault();
        }
    }
}
