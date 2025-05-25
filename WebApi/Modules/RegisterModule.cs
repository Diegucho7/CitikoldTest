using Carter;
using Microsoft.AspNetCore.Mvc;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.WebApi.Modules;

public class RegisterModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var register =  app.NewVersionedApi("Register");
        var group = register.MapGroup("api/v{version:apiVersion}")
            .HasApiVersion(1.0);
        
        group.MapPost("/Register", async (Users user, IRegisterService service) =>
        {
            var create = await service.CreateRegistrer(user);
            return Results.Created($"/Register  /{create.IsSuccess}", create);
        });
        group.MapPut("/Register", async (Users user, IRegisterService service) =>
        {
            var edit = await service.UpdateRegistre(user);
            return Results.Created($"/Register  /{edit.IsSuccess}", edit);
        });
        group.MapGet("/Register/{id}", async (int id, IRegisterService service) =>
        {
            var get = await service.ReadRegistre(id);
            return Results.Ok(get);
        });
        group.MapDelete("/Register/{id}", async (int id, IRegisterService service) =>
        {
            var delete = await service.DeleteRegistre(id);
            return Results.Ok(delete);
        });
        group.MapPatch("/Register", async (UpdatePasswordRequestDto password , IRegisterService service) =>
        {
            var updatePassword = await service.UpadatePassword(password);
            return Results.Ok(updatePassword);
        });
        group.MapPost("/Register/RestorePassword", async (RestorePasswordRequestDto email , IRegisterService service) =>
        {
            var restorePassword = await service.RestorePassword(email);
            return Results.Ok(restorePassword);
        });
        group.MapPost("/Register/RestoreChangePassword", async (ResetPasswordRequestDto email , IRegisterService service) =>
        {
            var restorePassword = await service.RestoreChangePassword(email);
            return Results.Ok(restorePassword);
        });
        group.MapPost("/Login", async ([FromBody]LogginRequestDto loggin , IRegisterService service) =>
        {
            var logginResponse = await service.Loggin(loggin);
            return Results.Ok(logginResponse);
        });
    }
}