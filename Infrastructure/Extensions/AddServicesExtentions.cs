
namespace RetailCitikold.Infrastructure.Extensions;

public static class AddServicesExtentions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        #region Services
        
        // services.AddScoped<IFacturaService, FacturaService>();
        // services.AddScoped<IRetailCitikoldService, RetailCitikoldService>();
        // services
        //     .AddScoped<ILiquidacionContratacionGastosExportacionService,
        //         LiquidacionContratacionGastosExportacionService>();
        //
        #endregion
        
        return services;
    }

    public static WebApplication UseServices(this WebApplication app)
    {
        return app;
    }
}
