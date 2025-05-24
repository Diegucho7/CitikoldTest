using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IItemService
{
    Task<ProcessResponseDto> CreateItem(Items item);
    Task<ItemResponseDto> ReadItem(int id);
    Task<ProcessResponseDto> UpdateItem(Items item);
    Task<ItemResponseDto> DeleteItem(int id);
   
}