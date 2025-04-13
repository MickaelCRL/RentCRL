using RentCRL.Domain.Users;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace RentCRL.Infrastructure.Users
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly CosmosDbService _cosmosDbService;
        private readonly Container _container;

        public OwnerRepository(CosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
            _container = _cosmosDbService.GetContainer("Owners").Result;
        }

        public async Task<Owner> AddAsync(Owner owner)
        {
            var response = await _container.CreateItemAsync(owner);
            return response.Resource;
        }

        public async Task<Owner> GetByEmailAsync(string email)
        {
            var feedIterator = _container.GetItemLinqQueryable<Owner>(
                                requestOptions: new QueryRequestOptions
                                {
                                    PartitionKey = null
                                })
                                .Where(o => o.Email == email)
                                .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.SingleOrDefault();
        }
    }
}
