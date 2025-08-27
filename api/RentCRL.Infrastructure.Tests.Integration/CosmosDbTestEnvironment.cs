using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using RentCRL.Infrastructure.Database;

namespace RentCRL.Infrastructure.Tests.Integration
{
    public class CosmosDbTestEnvironment : IDisposable
    {
        private CosmosDbCreator _cosmosDbCreator;

        public CosmosDbSettings Settings { get; private set; }
        public CosmosClient Client { get; private set; }

        public async Task EnsureDatabaseAndContainerExistAsync()
        {
            InitializeSettings();
            InitializeClient();
            InitializeCosmosDbCreator();
            await _cosmosDbCreator.EnsureDatabaseAndContainerExistAsync();
        }

        public async Task DeleteDatabaseAsync()
        {
            await Client.GetDatabase(Settings.DatabaseId).DeleteAsync();
        }

        public Container GetEntitiesContainer()
        {
            return Client.GetContainer(Settings.DatabaseId, ContainersNames.Entities);
        }

        public void Dispose()
        {
            _cosmosDbCreator = null;
            Client.Dispose();
        }

        private void InitializeSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.integration.json", optional: false)
                .Build();

            var cosmosDbSetting = configuration
                .GetSection(nameof(CosmosDbSettings))
                .Get<CosmosDbSettings>();

            cosmosDbSetting.DatabaseId = $"Test_{Guid.NewGuid()}";

            Settings = cosmosDbSetting;
        }

        private void InitializeClient()
        {
            Client = new CosmosClient(Settings.EndpointUri, Settings.PrimaryKey);
        }

        private void InitializeCosmosDbCreator()
        {
            _cosmosDbCreator = new CosmosDbCreator(Settings, Client);
        }

    }
}
