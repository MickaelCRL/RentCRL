namespace RentCRL.Infrastructure.Tests.Integration
{
    public class BaseTests
    {
        protected CosmosDbTestEnvironment _cosmosDbTestEnvironment;

        protected async Task BaseSetUp()
        {
            _cosmosDbTestEnvironment = new CosmosDbTestEnvironment();
            await _cosmosDbTestEnvironment.EnsureDatabaseAndContainerExistAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _cosmosDbTestEnvironment.DeleteDatabaseAsync();
            _cosmosDbTestEnvironment.Dispose();
        }
    }
}
