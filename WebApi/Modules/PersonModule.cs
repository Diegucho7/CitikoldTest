using Carter;
using Microsoft.AspNetCore.Mvc;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.WebApi.Modules;

public class PersonModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var register =  app.NewVersionedApi("Person");
        var group = register.MapGroup("api/v{version:apiVersion}")
            .HasApiVersion(1.0);
        
        group.MapGet("/Person", async (IPersonService service) =>
        {
            var get = await service.ReadPersonTotal();
            return Results.Ok(get);
        });       
    }
}