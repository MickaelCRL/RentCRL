using Microsoft.Azure.Cosmos;

public class CosmosDbService
{
    private readonly CosmosClient _client;
    private readonly string _databaseId;

    public CosmosDbService(string endpoint, string key, string databaseId)
    {
        _client = new CosmosClient(endpoint, key);
        _databaseId = databaseId;
    }

    public async Task<Database> GetDatabase()
    {
        return await _client.CreateDatabaseIfNotExistsAsync(_databaseId);
    }

    public async Task<Container> GetContainer(string containerId)
    {
        var database = await GetDatabase();
        return await database.CreateContainerIfNotExistsAsync(id: containerId, partitionKeyPath: "/id");
    }
}