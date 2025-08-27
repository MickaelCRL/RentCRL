using Microsoft.Azure.Cosmos;
using RentCRL.Infrastructure.Database;

public class CosmosDbCreator
{
    private readonly CosmosDbSettings _settings;
    private readonly CosmosClient _client;

    public CosmosDbCreator(CosmosDbSettings settings, CosmosClient client)
    {
        _settings = settings;
        _client = client;
    }

    public async Task EnsureDatabaseAndContainerExistAsync()
    {
        var database = await _client.CreateDatabaseIfNotExistsAsync(_settings.DatabaseId);

        await CreateAllContainersIfNotExistsAsync(database);
    }

    private static async Task CreateAllContainersIfNotExistsAsync(Database database)
    {
        foreach(var partitionKey in ContainersNames.PartitionKeys)
        {
            await database.CreateContainerIfNotExistsAsync(id: partitionKey.Key, partitionKeyPath: partitionKey.Value);
        }
    }
}