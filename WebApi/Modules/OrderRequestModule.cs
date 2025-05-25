using Carter;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.WebApi.Modules;

public class OrderRequestModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var orderRequest =  app.NewVersionedApi("OrderRequest");
        var group = orderRequest.MapGroup("api/v{version:apiVersion}")
            .HasApiVersion(1.0);
        
        group.MapPost("/OrderRequest", async (OrderRequestRequestDto order, IOrderRequestService service) =>
        {
            var create = await service.CreateOrder(order);
            return Results.Created($"/OrderRequest  /{create.IsSuccess}", create);
        });
    }
}