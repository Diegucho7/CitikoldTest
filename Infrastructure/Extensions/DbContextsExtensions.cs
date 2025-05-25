
using RetailCitikold.Domain.DataAccess.Context;

using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.DataAccess.Repositories;

namespace RetailCitikold.Infrastructure.Extensions;

public static class DbContextsExtensions
{
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IItemService, ItemRepository>();
        services.AddScoped<IRegisterService, RegisterRepository>();
        services.AddScoped<IEmailService, EmailRepository>();
   
        return services;
    }
    
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var RetailCitikoldConnectionString =
            Environment.GetEnvironmentVariable("RetailCitikold_CONNECTION_STRING")
            ?? configuration.GetConnectionString("RetailCitikoldConnection");
      
        ArgumentNullException.ThrowIfNull(RetailCitikoldConnectionString);
 
        services.AddDbContext<RetailCitikoldDbContext>(options =>
        {
            options.UseSqlServer(RetailCitikoldConnectionString, builder =>
            {
                builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
            });
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });

        
        
        _ = services.AddRepositories();

        return services;
    }

    public static WebApplication UseDbContexts(this WebApplication app)
    {
        return app;
    }
}
