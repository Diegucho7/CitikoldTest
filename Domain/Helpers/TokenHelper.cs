using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RetailCitikold.Domain.Dtos.Response;
using TokenValidationResult = RetailCitikold.Domain.Dtos.Response.TokenValidationResult;

namespace RetailCitikold.Domain.Helpers;

public class TokenHelper
{
    
    private readonly string _secret;

    public TokenHelper()
    {
        _secret = Environment.GetEnvironmentVariable("JWT_SECRET")
                  ?? throw new InvalidOperationException("JWT_SECRET environment variable not found.");
    }
    public string GeneratePasswordResetToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    
    public  TokenValidationResult  ValidatePasswordResetToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);
    
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // opcional: cambia a true si usas un issuer
            ValidateAudience = false, // opcional: cambia a true si usas audience
            ClockSkew = TimeSpan.Zero // opcional: elimina tolerancia extra de tiempo
        };
    
        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
    
            if (validatedToken is JwtSecurityToken jwtToken &&
                jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return new TokenValidationResult
                {
                    IsSuccess = true,
                    Principal = principal,
                    ErrorMessage =  string.Empty,                    
                };
            }
        }catch
        {
            
            return new TokenValidationResult
            {
                IsSuccess = true,
                Principal = null,
                ErrorMessage =  "Token is invalido",                    
            };
        }
        return new TokenValidationResult
        {
            IsSuccess = true,
            Principal = null,
            ErrorMessage =  "Token is invalido",                    
        };
    }
    public Token GenerateToken(string userId, string email, int expirationHours = 1)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secret);

        var expiration = DateTime.UtcNow.AddHours(expirationHours);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = expiration,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new Token(tokenString);
    }
}