using RetailCitikold.Infrastructure.Extensions;

namespace RetailCitikold.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        
        builder.Services
          
            .AddHttpClients()
            .AddDbContexts(builder.Configuration)
            .AddServices()
            .AddApiVersioningExtension()
            .AddCorsExtension()
            .AddAuthenticationMiddleware()
            .AddAuthorizationMiddleware()
            .AddCarterModules();
        
        builder.Services.AddEndpointsApiExplorer();
        // builder.Services.AddSwaggerGen();
        
        return builder;
    }
    
    public static WebApplication ConfigureApp(this WebApplication app)
    {
      

        app.UseApplicationSettings()
            .UseHttpClients()
            .UseDbContexts()
            .UseServices()
            .UseApiVersioningExtension()
            .UseCorsExtension()
            .UseAuthenticationMiddleware()
            .UseAuthorizationMiddleware();
        
        // app.UseHttpsRedirection();

        app.UseCarterModules();

        return app;
    }
}
