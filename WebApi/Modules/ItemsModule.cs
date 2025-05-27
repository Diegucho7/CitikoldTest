using Carter;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.WebApi.Modules;

public class ItemsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var items =  app.NewVersionedApi("Items");
        var group = items.MapGroup("api/v{version:apiVersion}")
            .HasApiVersion(1.0);

        group.MapPost("/Item", async (Items item, IItemService service) =>
        {
            var create = await service.CreateItem(item);
            return Results.Created($"/item/{create.IsSuccess}", create);
        });
        group.MapGet("/Item", async (IItemService service) =>
        {
            var get = await service.ReadItemTotal();
            return Results.Ok(get);
        });       
        group.MapPut("/Item/{id}", async (Items item, IItemService service) =>
                {
            var edit = await service.UpdateItem(item);
            return Results.Created($"/Item/{edit.IsSuccess}", edit);
        });
        group.MapGet("/Item/{id}", async (int id, IItemService service) =>
        {
            var get = await service.ReadItem(id);
            return Results.Ok(get);
        });
        group.MapDelete("/Item/{id}", async (int id, IItemService service) =>
        {
            var delete = await service.DeleteItem(id);
            return Results.Ok(delete);
        });
    }
}

