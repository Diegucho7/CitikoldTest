namespace RetailCitikold.Domain.Dtos.Request
{
    public record UpdatePasswordRequestDto(string Username, string CurrentPassword, string Password);
    public record RestorePasswordRequestDto(string Email);

}

