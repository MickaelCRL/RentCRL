using RentCRL.Domain.Users;
using Microsoft.Azure.Cosmos;


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
    }
}
