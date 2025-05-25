namespace RetailCitikold.Domain.Dtos.Request;

public record ResetPasswordRequestDto
{
    public string Token { get; set; } = "";
    public string NewPassword { get; set; } = "";
}