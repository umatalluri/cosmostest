using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Optional: Register services here
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello from Azure App Service + CosmosDB");

app.Run();
