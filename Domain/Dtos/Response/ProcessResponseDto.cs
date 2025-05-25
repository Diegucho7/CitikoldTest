using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.Dtos.Response
{
    public record ProcessResponseDto(bool IsSuccess = true, string Error = "");
    public record LogginResponseDto(bool IsSuccess = true, string Mssg = "", Token? Token = null);

    public record ItemResponseDto( bool IsSuccess = true, string Error = "", Items? Item = default);
    public record UserResponseDto( bool IsSuccess = true, string Error = "", Users? Users = default);
    
            

    
}

