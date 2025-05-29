using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IOrderRequestService
{
    Task<ProcessResponseDto> CreateOrder(List<OrderRequestUtilRequestDto> order);
    Task<OrderRequestWithClientDto> ReadOrder(int id);
    Task<List<Object>> ReadAllOrder();
    Task<ProcessResponseDto> UpdateOrder(int id, List<OrderRequestUtilRequestDto> order);

    Task<OrderResponseDto> DeleteOrder(int id);
}