using Azure.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

public class CosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly string _accountUri;

    public CosmosDbService(IConfiguration configuration)
    {
        _accountUri = configuration["CosmosDb:AccountUri"];
        _databaseName = configuration["CosmosDb:DatabaseName"];

        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = "7318c4ba-8710-479b-a4da-9551373b71a1"
        });

        _cosmosClient = new CosmosClient(_accountUri, credential);
    }

    public async Task<string> TestConnectionAsync()
    {
        try
        {
            // Use CreateDatabaseIfNotExistsAsync to test the connection (no GetDatabaseAsync exists)
            DatabaseResponse response = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
            return $"Connection to Cosmos DB successful! Database ID: {response.Database.Id}";
        }
        catch (Exception ex)
        {
            return $"Error connecting to Cosmos DB: {ex.Message}";
        }
    }
}
