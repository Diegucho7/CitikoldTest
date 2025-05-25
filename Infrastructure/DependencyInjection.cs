
using Microsoft.Extensions.Options;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.DataAccess.Repositories;
using RetailCitikold.Domain.Dtos;
using RetailCitikold.Domain.Helpers;
using RetailCitikold.Infrastructure.Extensions;


namespace RetailCitikold.Infrastructure;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<PasswordValidatorLocal>();
        builder.Services.AddScoped<TokenHelper>();
        builder.Services.AddHttpContextAccessor();
        DotNetEnv.Env.Load();

        // Program.cs o Startup.cs (dependiendo tu versión)
        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

// Registra el repositorio que depende de IOptions<SmtpSettings>
        builder.Services.AddScoped<IEmailService, EmailRepository>();
        
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
