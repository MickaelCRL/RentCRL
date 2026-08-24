using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Contracts;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;
using Serilog;

namespace RentCRL.Infrastructure.Contracts
{
    public class ContractRepository : EntityRepository<Contract>, IContractRepository
    {
        public ContractRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient, ILogger logger)
            : base(cosmosDbSettings, cosmosClient, logger)
        { }

        public async Task<List<Contract>> GetContractsByOwnerIdAsync(Guid ownerId)
        {
            var feedIterator = GetContainer().GetItemLinqQueryable<Contract>(
                              requestOptions: new QueryRequestOptions
                              {
                                  PartitionKey = null
                              })
                              .Where(c => c.OwnerId == ownerId && c.EntityType == nameof(Contract))
                              .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.ToList();
        }
    }
}