using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add CosmosDbService as a singleton
builder.Services.AddSingleton<CosmosDbService>();

var app = builder.Build();

app.MapGet("/", async (CosmosDbService cosmosDbService) =>
{
    var result = await cosmosDbService.TestConnectionAsync();
    return Results.Text(result);
});

app.Run();
