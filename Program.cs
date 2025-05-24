using RetailCitikold.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureServices();

var app = builder.Build();
app.ConfigureApp();
await app.RunAsync();