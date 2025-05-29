using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.Dtos.Response
{
    public record ProcessResponseDto(bool IsSuccess = true, string Mssg = "");
    public record OrderResponseDeleteDto(bool IsSuccess = true, string Mssg = "");
    public record LogginResponseDto(bool IsSuccess = true, string Mssg = "", Token? Token = null);
    public record ValidTokenResponseDto(
        bool IsSuccess = true,
        string Mssg = "",
        List<UsuarioDto>? Usuario = null,
        Token? Token = null
    );
    public record UsuarioDto(
        int IdUser,
        string Email,
        string Name,
        string Lastname,
        string Role,
        string Img,
        int IdGroup
    );
    public record ItemResponseDto( bool IsSuccess = true, string Error = "", Items? Item = default);
    public record ItemResponseTotalDto(  List<Items> Item = default);
    
    public record ResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public record PersonResponseTotalDto(  List<Person> Person = default);
    public record OrderResponseDto(bool IsSuccess = true, string Error = "", OrderRequest? OrderRequest = default);
    public record UserResponseDto( bool IsSuccess = true, string Mssg = "", Users? Users = default);
    
            

    
}

