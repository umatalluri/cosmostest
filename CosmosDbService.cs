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

    // Constructor to initialize CosmosDbService
    public CosmosDbService(IConfiguration configuration)
    {
        // Get Cosmos DB URI and Database Name from configuration
        _accountUri = configuration["CosmosDb:AccountUri"];  // Cosmos DB account URI
        _databaseName = configuration["CosmosDb:DatabaseName"]; // Cosmos DB Database Name

        // Use DefaultAzureCredential for Managed Identity Authentication
        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = "7318c4ba-8710-479b-a4da-9551373b71a1" // Managed Identity Client ID
        });

        // Initialize CosmosClient using the account URI and Managed Identity credential
        _cosmosClient = new CosmosClient(_accountUri, credential);
    }

    // Method to test connection to Cosmos DB
    public async Task<string> TestConnectionAsync()
    {
        try
        {
            // Attempt to get the Cosmos DB database
            var database = await _cosmosClient.GetDatabaseAsync(_databaseName);
            return "Connection to Cosmos DB successful!";
        }
        catch (Exception ex)
        {
            return $"Error connecting to Cosmos DB: {ex.Message}";
        }
    }
}
