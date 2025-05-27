using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IRegisterService
{
    Task<ProcessResponseDto> CreateRegistrer(Users item);
    Task<UserResponseDto> ReadRegistre(int id);
    Task<ProcessResponseDto> UpdateRegistre(Users item);
    Task<ProcessResponseDto> DeleteRegistre(int id);
    Task<ProcessResponseDto> UpadatePassword(UpdatePasswordRequestDto password  );
        
    Task<ProcessResponseDto> RestorePassword(RestorePasswordRequestDto email);
    Task<ProcessResponseDto> RestoreChangePassword(ResetPasswordRequestDto email);
    Task<LogginResponseDto> Loggin(LogginRequestDto loggin);
    Task<ValidTokenResponseDto> ValidToken(string token);
}