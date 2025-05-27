using RetailCitikold.Infrastructure;


var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

// Agregar variables de entorno al Configuration
builder.Configuration.AddEnvironmentVariables();

// Ya podes leer la variable con:
var secret = builder.Configuration["JWT_SECRET"];


// var secret = Environment.GetEnvironmentVariable("JWT_SECRET");
builder.ConfigureServices();




var app = builder.Build();
app.ConfigureApp();
await app.RunAsync();