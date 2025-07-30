using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using RentCRL.Infrastructure.Database;

namespace RentCRL.Infrastructure.Tests.Integration
{
    public class CosmosDbTestEnvironment
    {
        public CosmosDbSettings Settings { get; private set; }
        public CosmosClient Client { get; private set;  }
        public CosmosDbService Service { get; private set; }

        public CosmosDbTestEnvironment()
        {
            Settings = LoadSettings();
            Client = CreateClient();
            Service = CreateService();
        }

        public CosmosDbService CreateService()
        {
            return new CosmosDbService(Settings, Client);
        }

        public CosmosClient CreateClient()
        {
            return new CosmosClient(Settings.EndpointUri, Settings.PrimaryKey);
        }

        public CosmosDbSettings LoadSettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.integration.json", optional: false)
                .Build();

            Console.WriteLine(AppContext.BaseDirectory);

            var cosmosDbSetting = configuration
                .GetSection(nameof(CosmosDbSettings))
                .Get<CosmosDbSettings>();

            cosmosDbSetting.DatabaseId = $"Test_{Guid.NewGuid()}";

            return cosmosDbSetting;
        }

        public async Task DeleteAndDisposeAsync()
        {
            await Client.GetDatabase(Settings.DatabaseId).DeleteAsync();
            Client.Dispose();
        }
    }
}
