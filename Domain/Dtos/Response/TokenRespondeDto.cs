using System.Security.Claims;

namespace RetailCitikold.Domain.Dtos.Response;


    public record Token(string Value);
    public record TokenValidationResult
    {
        public bool IsSuccess { get; init; }
        public ClaimsPrincipal? Principal { get; init; }
        public string Email { get; set; } // <-- agregar esto
        public string? ErrorMessage { get; init; }
    }