using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.Dtos.Response
{
    public record ProcessResponseDto(bool IsSuccess = true, string Error = "");

    public record ItemResponseDto( bool IsSuccess = true, string Error = "", Items? Item = default);
    
            

    
}

