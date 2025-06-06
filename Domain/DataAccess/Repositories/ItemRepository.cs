using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Context;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class ItemRepository(RetailCitikoldDbContext context) : IItemService
{
    
    #region Create Item
    public async Task<ItemResponseDto> CreateItem(Items item)
    {

        try
        {
          _ =  await context.Items.AddAsync(item);
          _ =  await context.SaveChangesAsync();
          return new ItemResponseDto
          {
              IsSuccess = true,
              Error = "Producto ingresado correctamente",
              Item =  item
              
          };
        }
        catch (Exception ex)
        {
            return new ItemResponseDto
            {
                IsSuccess = false,
                Error = $"Error al guardar el item: {ex.InnerException?.Message ?? ex.Message}",
                Item =  null
            };
        }
       
    }
    #endregion

    #region Read Item {id}
    
    public async Task<ItemResponseDto> ReadItem(int id)
    {
        var item = await context.Items.FirstOrDefaultAsync(i => i.id == id);
        if (item == null)
        {
            return new ItemResponseDto
            {
                IsSuccess = false,
                Error = "Item no encontrado",
                Item = default
            };
        }
        return new ItemResponseDto
        {
            IsSuccess = true,
            Error = "Item encontrado",
            Item = item
        };
    }

   

    #endregion

    #region Get All Items

    
        public async Task<ItemResponseTotalDto> ReadItemTotal()
        {
            var item = await context.Items.ToListAsync();
            if (item == null || item.Count == 0)
            {
                return new ItemResponseTotalDto
                {
                  
                    Item = default
                };
            }
            return new ItemResponseTotalDto
            {
               
                Item = item 
            };
        }

    #endregion

    #region Update Iten
    public async Task<ProcessResponseDto> UpdateItem(Items item)
    {
        var ifExist = context.Items.FirstOrDefault(i => i.id == item.id);
        if (ifExist == null)
        {
            
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "Item no encontrado"
            };
        }
        
        // Validaciones
        if (item.price <= 0)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "El precio debe ser mayor a cero"
            };
        }

        if (string.IsNullOrWhiteSpace(item.name))
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "El nombre no puede estar vacío"
            };
        }

        if (item.stock < 0)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = "El stock no puede ser negativo"
            };
        }
        
        
        try
        {
            
            ifExist.code = item.code;
            ifExist.name = item.name;
            ifExist.id_State = item.id_State;
            ifExist.description = item.description;
            ifExist.id_ProductType = item.id_ProductType;
            ifExist.id_Brand = item.id_Brand;
            ifExist.price = item.price;
            ifExist.id_CountryOrigin = item.id_CountryOrigin;
            ifExist.stock = item.stock;

            await context.SaveChangesAsync();
            
            return new ProcessResponseDto
            {
                IsSuccess = true,
                Mssg = ""
            };
            
        }
        catch (Exception ex)
        {
            return new ProcessResponseDto
            {
                IsSuccess = false,
                Mssg = $"Error al guardar el item: {ex.InnerException?.Message ?? ex.Message}"
            };
            
        }
        
    }
    
    
    
    #endregion

    #region Delete Item

    public async Task<ItemResponseDto> DeleteItem(int id)
    {
        var item = await context.Items.FirstOrDefaultAsync(i => i.id == id);
        if (item == null)
        {
            return new ItemResponseDto(
                IsSuccess: false,
                Error: "Item no encontrado",
                Item: default
            );
        }

        try
        {
            context.Items.Remove(item);
            await context.SaveChangesAsync();

            return new ItemResponseDto(
                IsSuccess: true,
                Error: "Se elimino el item",
                Item: item
            );
        }
        catch (Exception ex)
        {
            return new ItemResponseDto(
                IsSuccess: false,
                Error: $"Error al eliminar el item: {ex.Message}",
                Item: null
            );
        }
    }

    #endregion
    
    
    
}