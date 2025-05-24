
using Microsoft.Extensions.Options;

namespace RetailCitikold.Infrastructure.Extensions;

public static class ApplicationSettingsExtensions
{
    // public static IServiceCollection AddApplicationSettings(this IServiceCollection services)
    // {
    //
    //     
    //
    // }
    
    public static WebApplication UseApplicationSettings(this WebApplication app)
    {
        return app;
    }
}
