

namespace RetailCitikold.Infrastructure.Extensions;

public static class HttpClientsExtensions
{
    public static IServiceCollection AddHttpClients(this IServiceCollection services)
    {
 
     
        
        return services;
    }

    public static WebApplication UseHttpClients(this WebApplication app)
    {
        return app;
    }
}
