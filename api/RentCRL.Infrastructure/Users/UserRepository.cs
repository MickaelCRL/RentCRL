using RentCRL.Domain.Users;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using RentCRL.Infrastructure.Base;

namespace RentCRL.Infrastructure.Users
{
    public class UserRepository : EntityRepository<Domain.Users.User>, IUserRepository
    {
        public UserRepository(CosmosDbService cosmosDbService)
            : base(cosmosDbService)
        { }

        public async Task<Domain.Users.User> GetByEmailAsync(string email)
        {
            var feedIterator = _container.GetItemLinqQueryable<Domain.Users.User>(
                               requestOptions: new QueryRequestOptions
                               {
                                   PartitionKey = null
                               })
                                     .Where(u => u.Email == email && u.EntityType == nameof(Owner) || u.EntityType == nameof(Tenant))
                               .ToFeedIterator();

            var response = await feedIterator.ReadNextAsync();
            return response.SingleOrDefault();
        }
    }
}
