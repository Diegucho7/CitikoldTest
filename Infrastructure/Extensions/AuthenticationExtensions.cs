using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace RetailCitikold.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationMiddleware(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
//                 var ssoSettings = builder.Services.BuildServiceProvider().GetRequiredService<IDispAuthSsoSettings>();
//
//                 var metadataAddress = ssoSettings.MetadataAddress();
//                 
//                 var tokenIssuers = CoreEnvironment.TokenIssuer.GetEnvironmentVariable(string.Empty);
//                 var validIssuers = !string.IsNullOrEmpty(tokenIssuers) ? tokenIssuers.Split(",") : null;
//                 
//                 var tokenAudiences = CoreEnvironment.TokenAudience.GetEnvironmentVariable(string.Empty);
//                 var validAudiences = !string.IsNullOrEmpty(tokenAudiences) ? tokenAudiences.Split(",") : null;
//
//                 options.MetadataAddress = metadataAddress;
//                 options.SaveToken = true;
//                 options.RequireHttpsMetadata = ssoSettings.Scheme.Equals("https", StringComparison.Ordinal);
//     
//                 options.TokenValidationParameters = new TokenValidationParameters
//                 {
//                     ClockSkew = TimeSpan.Zero,
//                     ValidateAudience = (validAudiences is not null && validAudiences.Length > 0),
//                     ValidAudiences = validAudiences,
//                     ValidateIssuer = (validIssuers is not null && validIssuers.Length > 0),
//                     ValidIssuers = validIssuers,
//                     ValidateLifetime = true,
//                     ValidateIssuerSigningKey = true,
//                     ValidateTokenReplay = true,
//                     ValidateSignatureLast = true,
//                     LogValidationExceptions = true,
//                 };
//     
//                 options.Events = new()
//                 {
//                     OnMessageReceived = _ => Task.CompletedTask,
//                     OnChallenge = _ => Task.CompletedTask,
//                     OnForbidden = _ => Task.CompletedTask,
//                     OnAuthenticationFailed = (context) =>
//                     {
//                         var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<AuthenticationMiddleware>>();
// #pragma warning disable CA1848
//                         logger.LogError(context.Exception, "{ErrorMessage}", context.Exception.InnerException?.Message ?? context.Exception.Message);
// #pragma warning restore CA1848
//                         return Task.CompletedTask;
//                     },
//                     OnTokenValidated = async (context) => await OnTokenValidated(context).ConfigureAwait(false),
//                 };
//
//                 IdentityModelEventSource.LogCompleteSecurityArtifact = true;
//                 IdentityModelEventSource.ShowPII = true;
            });

        // var enabledKeyRotation = CoreEnvironment.EnabledKeyRotation.GetEnvironmentVariable(false);
        // if (enabledKeyRotation)
        // {
        //     builder.Services.PostConfigure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
        //     {
        //         if (options.ConfigurationManager is ConfigurationManager<OpenIdConnectConfiguration> manager)
        //         {
        //             var refreshInterval = CoreEnvironment.RotationPeriod.GetEnvironmentVariable(
        //                 TimeSpan.FromMinutes(1));
        //             manager.AutomaticRefreshInterval = refreshInterval;
        //         }
        //     });    
        // }

        return services;
    }
    
    public static WebApplication UseAuthenticationMiddleware(this WebApplication app)
    {
        app.UseAuthentication();
        
        return app;
    }
    
}

public static class HttpContextAccessorExtensions
{
    public static Guid UserId(this TokenValidatedContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var claimsPrincipal = context.Principal;
        
        if (claimsPrincipal?.Identity is null || !claimsPrincipal.Identity.IsAuthenticated)
        {
            return Guid.Empty;
        }
        
        var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if(claim is null || string.IsNullOrEmpty(claim.Value)) return Guid.Empty;
        return Guid.TryParse(claim.Value, out var userId) ? userId : Guid.Empty;
    }
    
    public static Guid SessionId(this TokenValidatedContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var claimsPrincipal = context.Principal;
        
        if (claimsPrincipal?.Identity is null || !claimsPrincipal.Identity.IsAuthenticated)
        {
            return Guid.Empty;
        }
        
        var claim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Sid);
        if(claim is null || string.IsNullOrEmpty(claim.Value)) return Guid.Empty;
        return Guid.TryParse(claim.Value, out var sessionId) ? sessionId : Guid.Empty;
    }
    
    public static string ClientIpAddress(this TokenValidatedContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var httpContext = context.HttpContext;
        return GetRemoteHostIpAddressUsingXRealIp(httpContext)?.ToString()
               ?? GetRemoteHostIpAddressUsingXForwardedFor(httpContext)?.ToString()
               ?? GetRemoteHostIpAddressUsingRemoteIpAddress(httpContext)?.ToString() 
               ?? string.Empty;
    }

    public static string UserAgent(this TokenValidatedContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        
        var httpContext = context.HttpContext;
        var request = httpContext.Request;
        var userAgentHeader = request.Headers.UserAgent;
        return userAgentHeader.ToString();
    }
    
    private static IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext httpContext)
    {
        var ipAddress = httpContext.Connection.RemoteIpAddress;
        if (ipAddress?.ToString() == "::1")
        {
            return IPAddress.Parse("127.0.0.1");
        }
        return httpContext.Connection.RemoteIpAddress?.IsIPv4MappedToIPv6 == true 
            ? httpContext.Connection.RemoteIpAddress?.MapToIPv4() 
            : httpContext.Connection.RemoteIpAddress;
    }
    
    private static IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext httpContext)
    {
        IPAddress? remoteIpAddress = null;
        var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrEmpty(forwardedFor))
        {
            var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim());
            foreach (var ip in ips)
            {
                if (IPAddress.TryParse(ip, out var address) &&
                    (address.AddressFamily is AddressFamily.InterNetwork
                        or AddressFamily.InterNetworkV6))
                {
                    remoteIpAddress = address;
                    break;
                }
            }
        }
        return remoteIpAddress?.IsIPv4MappedToIPv6 == true 
            ? remoteIpAddress.MapToIPv4() 
            : remoteIpAddress;
    }
    
    private static IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext httpContext)
    {
        IPAddress? remoteIpAddress = null;
        var xRealIpExists = httpContext.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp);
        if (!xRealIpExists)
        {
            return remoteIpAddress;
        }

        if (!IPAddress.TryParse(xRealIp, out IPAddress? address))
        {
            return remoteIpAddress;
        }
        
        var isValidIp = (address.AddressFamily is AddressFamily.InterNetwork
            or AddressFamily.InterNetworkV6);
        
        if (isValidIp)
        {
            remoteIpAddress = address;
        }
        
        return remoteIpAddress?.IsIPv4MappedToIPv6 == true 
            ? remoteIpAddress.MapToIPv4() 
            : remoteIpAddress;

    }
}
