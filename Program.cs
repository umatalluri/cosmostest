var builder = WebApplication.CreateBuilder(args);

// Add CosmosDbService to DI container
builder.Services.AddSingleton<CosmosDbService>();

var app = builder.Build();

// Endpoint to test Cosmos DB connectivity
app.MapGet("/", async (CosmosDbService cosmosDbService) =>
{
    var result = await cosmosDbService.TestConnectionAsync();
    return Results.Ok(result);  // Return success or error message based on Cosmos DB connection status
});

app.Run();
