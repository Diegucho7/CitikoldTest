using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.Dtos.Response
{
    public record ProcessResponseDto(bool IsSuccess = true, string Mssg = "");
    public record LogginResponseDto(bool IsSuccess = true, string Mssg = "", Token? Token = null);

    public record ItemResponseDto( bool IsSuccess = true, string Error = "", Items? Item = default);
    public record OrderResponseDto(bool IsSuccess = true, string Error = "", OrderRequest? OrderRequest = default);
    public record UserResponseDto( bool IsSuccess = true, string Mssg = "", Users? Users = default);
    
            

    
}

