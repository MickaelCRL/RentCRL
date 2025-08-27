using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Domain.Users;
using RentCRL.Infrastructure.Base;
using RentCRL.Infrastructure.Database;

namespace RentCRL.Infrastructure.Users
{
    public class UserRepository : EntityRepository<Domain.Users.User>, IUserRepository
    {
        public UserRepository(CosmosDbSettings cosmosDbSettings, CosmosClient cosmosClient)
            : base(cosmosDbSettings, cosmosClient)
        { }

        public async Task<Domain.Users.User> GetByEmailAsync(string email)
        {
            var feedIterator = GetContainer()
                .GetItemLinqQueryable<Domain.Users.User>(requestOptions: new QueryRequestOptions
                {
                    PartitionKey = null
                })
                .Where(u => u.Email == email && (u.EntityType == nameof(Owner) || u.EntityType == nameof(Tenant)))
                .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.SingleOrDefault();
        }
    }
}
