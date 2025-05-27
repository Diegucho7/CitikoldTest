// AuthenticationExtensions.cs

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationMiddleware(this IServiceCollection services, IConfiguration configuration)
    {
        var secret = configuration["JWT_SECRET"];

        if (string.IsNullOrEmpty(secret))
            throw new InvalidOperationException("JWT Secret not found in configuration");

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    public static WebApplication UseAuthenticationMiddleware(this WebApplication app)
    {
        app.UseAuthentication();
        return app;
    }
}
