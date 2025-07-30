using Microsoft.Azure.Cosmos;
using RentCRL.Infrastructure.Database;

public class CosmosDbService
{
    private readonly CosmosDbSettings _settings;
    private readonly CosmosClient _client;

    public CosmosDbService(CosmosDbSettings settings, CosmosClient client)
    {
        _settings = settings;
        _client = client;
    }

    public Container GetEntitiesContainer()
    {
        return _client.GetContainer(_settings.DatabaseId, ContainersNames.Entities);
    }

    public async Task EnsureDatabaseAndContainerExistAsync()
    {
        var database = await CreateDatabaseIfNotExistsAsync();
        await CreateContainerIfNotExistsAsync(database);
    }

    private async Task<Database> CreateDatabaseIfNotExistsAsync()
    {
        return await _client.CreateDatabaseIfNotExistsAsync(_settings.DatabaseId);
    }

    private static async Task CreateContainerIfNotExistsAsync(Database database)
    {
        foreach(var partitionKey in ContainersNames.PartitionKeys)
        {
            await database.CreateContainerIfNotExistsAsync(id: partitionKey.Key, partitionKeyPath: partitionKey.Value);
        }
    }
}