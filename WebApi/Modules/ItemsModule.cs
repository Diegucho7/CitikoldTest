using Carter;
using Microsoft.AspNetCore.Mvc;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.DataAccess.Repositories;
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
            return Results.Created($"/item  /{create.IsSuccess}", create);
        });
        group.MapPut("/Item", async (Items item, IItemService service) =>
        {
            var edit = await service.UpdateItem(item);
            return Results.Created($"/item  /{edit.IsSuccess}", edit);
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

