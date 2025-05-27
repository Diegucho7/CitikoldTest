using RetailCitikold.Domain.Dtos.Response;

namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IPersonService
{
    Task<PersonResponseTotalDto> ReadPersonTotal();
}