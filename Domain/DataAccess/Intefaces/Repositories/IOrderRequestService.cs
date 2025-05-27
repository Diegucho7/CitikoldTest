using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Intefaces.Repositories;

public interface IOrderRequestService
{
    Task<ProcessResponseDto> CreateOrder(List<OrderRequestUtilRequestDto> order);
    Task<OrderResponseDto> ReadOrder(int id);
    Task<List<Object>> ReadAllOrder();
    Task<ProcessResponseDto> UpdateOrder(OrderRequestRequestDto order);
    Task<OrderResponseDto> DeleteOrder(int id);
}