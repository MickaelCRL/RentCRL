using RentCRL.Domain.Users;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Infrastructure.Base;

namespace RentCRL.Infrastructure.Users
{
    public class OwnerRepository : EntityRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(CosmosDbService cosmosDbService)
            : base(cosmosDbService)
        { }

        public async Task<Owner> GetByEmailAsync(string email)
        {
            var feedIterator = _container.GetItemLinqQueryable<Owner>(
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
