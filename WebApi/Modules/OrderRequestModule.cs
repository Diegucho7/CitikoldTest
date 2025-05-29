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
        
        group.MapPost("/OrderRequest", async (List<OrderRequestUtilRequestDto> order, IOrderRequestService service) =>
        {
            var create = await service.CreateOrder(order);
            return Results.Created($"/OrderRequest /{create.IsSuccess}", create);
        });
        group.MapGet("/OrderRequest", async (IOrderRequestService service) =>
        {
            var get = await service.ReadAllOrder();
            return Results.Ok(get);
        });  
        group.MapGet("/OrderRequest/{id}", async (int id, IOrderRequestService service) =>
        {
            var get = await service.ReadOrder(id);
            return Results.Ok(get);
        }); 
        
        group.MapPut("/OrderRequest/{id}", async (int id, List<OrderRequestUtilRequestDto> order, IOrderRequestService service) =>
        {
            var edit = await service.UpdateOrder(id,order);
            return Results.Created($"/OrderRequest/{edit.IsSuccess}", edit);
        });
    }
}