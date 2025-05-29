using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Context;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class OrderRequestRepository(RetailCitikoldDbContext context) : IOrderRequestService
{
    
    #region Venta
    public async Task<ProcessResponseDto> CreateOrder(List<OrderRequestUtilRequestDto> orderDetail){
        
        var strategy = context.Database.CreateExecutionStrategy();


        var result = await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Obtener siguiente secuencial
                    var lastSecuencial = await context.OrderRequest
                        .OrderByDescending(d => d.secuencial)
                        .FirstOrDefaultAsync();

                    int siguienteSecuencial = (lastSecuencial?.secuencial ?? 0) + 1;

                    // Construir lista de detalles
                    List<OrderRequestDetails> detailsList = new List<OrderRequestDetails>();

                    foreach (var detail in orderDetail)
                    {
                        var orderDetail = new OrderRequestDetails
                        {
                            item_id = detail.item_id,
                            item_name = detail.item_name,
                            discount_percent = detail.discount_percent,
                            discount_value = detail.discount_value,
                            subtotal_before_tax = detail.subtotal_before_tax,
                            total_units = detail.total_units,
                            price = detail.price,
                            iva = detail.iva,
                            iva_value = detail.iva_value,
                            igv = detail.igv,
                            igv_value = detail.igv_value,
                            totalIgv = detail.totalIgv,
                            totalIva = detail.totalIva
                        };

                        detailsList.Add(orderDetail);
                    }
                    // Calcular totales
                    var totalDiscountValue = detailsList.Sum(x => x.discount_value ?? 0);
                    var totalSubtotalBeforeTax = detailsList.Sum(x => x.subtotal_before_tax);
                    var totalIvaValue = detailsList.Sum(x => x.iva_value ?? 0);
                    var totalIgvValue = detailsList.Sum(x => x.igv_value);
                    var totalIgv = detailsList.Sum(x => x.totalIgv);
                    var totalIva = detailsList.Sum(x => x.totalIva);


                    var document = new OrderRequest
                    {
                        id_client = orderDetail.First().id_client,
                        id_state = 1,
                        secuencial = siguienteSecuencial,
                        baseNoObjectIva = 0,
                        baseTax = totalSubtotalBeforeTax,
                        descount = totalDiscountValue,
                        grossSubtotal = totalSubtotalBeforeTax,
                        valueIva = totalIvaValue,
                        valueIgv = totalIgvValue,
                        totalIgv = totalIgv,
                        totalIva = totalIva,
                        numDocument = $"001-001-{siguienteSecuencial:D9}"
                    };

                    await context.OrderRequest.AddAsync(document);
                    await context.SaveChangesAsync();

                    foreach (var detail in detailsList)
                    {
                        detail.orderRequest_id = document.id;

                        var item = await context.Items.FirstOrDefaultAsync(i => i.id == detail.item_id);
                        if (item == null)
                            throw new Exception($"Item con ID {detail.item_id} no existe.");

                        if (item.stock < detail.total_units)
                            throw new Exception($"Stock insuficiente para el item {item.name}.");

                        item.stock -= detail.total_units;

                        await context.OrderRequestDetails.AddAsync(detail);
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new ProcessResponseDto
                    {
                        IsSuccess = true,
                        Mssg = "Orden registrada con éxito."
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    return new ProcessResponseDto
                    {
                        IsSuccess = false,
                        Mssg = $"Error en transacción: {ex.Message}"
                    };
                }
            }
        });
        return new ProcessResponseDto
        {
            IsSuccess = false,
            Mssg = $"Error en transacción: {result}"
        };
    }

    #endregion

    #region ReadOrder

    
     public async Task<OrderRequestWithClientDto> ReadOrder(int id)
    {
        try
        {
            var result = await (
                from order in context.OrderRequest
                join person in context.Person on order.id_client equals person.id
               
                where order.id == id
                
                select new OrderRequestWithClientDto
                {
                    Id = order.id,
                    IdPaymentTime = order.id_paymentTime,
                    BaseNoObjectIva = order.baseNoObjectIva,
                    BaseExemptIva = order.baseExemptIva,
                    BaseIvaZero = order.baseIvaZero,
                    BaseTax = order.baseTax,
                    Descount = order.descount,
                    GrossSubtotal = order.grossSubtotal,
                    ValueIva = order.valueIva,
                    ValueIce = order.valueIce,
                    TotalIva = order.totalIva,
                    TotalIgv = order.totalIgv,
                    ValueIgv = order.valueIgv,
                    IdClient = order.id_client,
                    NumDocument = order.numDocument,
                    
                    NumberIdentification = person.numberIdentification,
                    apellidoPaterno = person.apellidoPaterno,
                    apellidoMaterno = person.apellidoMaterno,
                    primerNombre = person.primerNombre,
                    segundoNombre = person.segundoNombre,
                    
                      }).FirstOrDefaultAsync();

            if (result != null)
            {
                var orderDetails = await context.OrderRequestDetails
                    .Where(od => od.orderRequest_id == id)
                    .Select(d => new OrderDetailsDTO
                    {
                        item_id = d.item_id,
                        item_name = d.item_name,
                        total_units = d.total_units,
                        price = d.price,
                        discount_percent = d.discount_percent,
                        discount_value = d.discount_value,
                        subtotal_before_tax = d.subtotal_before_tax,
                        iva = d.iva,
                        iva_value = d.iva_value,
                        igv = d.igv,
                        igv_value = d.igv_value,
                        totalIva = d.totalIva,
                        totalIgv = d.totalIgv
                    }).ToListAsync();

                result = result with { Items = orderDetails };
            }

          
            return result; //
        }
        catch (Exception ex)
        {
           
            return null;
        }
    
    }
    #endregion
   

    #region Read all order requests
    public async Task<List<Object>> ReadAllOrder()
    {
        try
        {

            var orders = await context.OrderRequest.ToListAsync();

            // Sacar los clientes relacionados solo de esas órdenes
            var result = await (from order in context.OrderRequest
                join person in context.Person
                    on order.id_client equals person.id
                select new OrderRequestWithClientDto
                {
                    Id = order.id,
                    primerNombre = person.primerNombre,
                    segundoNombre = person.segundoNombre,
                    apellidoMaterno = person.apellidoMaterno,
                    apellidoPaterno = person.apellidoPaterno,
                    IdPaymentTime = order.id_paymentTime,
                    BaseNoObjectIva = order.baseNoObjectIva,
                    BaseExemptIva = order.baseExemptIva,
                    BaseIvaZero = order.baseIvaZero,
                    BaseTax = order.baseTax,
                    Descount = order.descount,
                    GrossSubtotal = order.grossSubtotal,
                    ValueIva = order.valueIva,
                    ValueIce = order.valueIce,
                    TotalIva = order.totalIva,
                    TotalIgv = order.totalIgv,
                    ValueIgv = order.valueIgv,
                    IdClient = order.id_client,
                    NumDocument = order.numDocument,
                    NumberIdentification = person.numberIdentification,
                }).ToListAsync();
            
           

            return new List<Object>
            {
                result
            };

        }
        catch (Exception e)
        {
            return new List<Object>();
        }

    }
    
    #endregion
    
    
    #region UpdateOrderRequest
    
    public async Task<ProcessResponseDto> UpdateOrder(int id, List<OrderRequestUtilRequestDto> orderDetail)
    {
        var strategy = context.Database.CreateExecutionStrategy();

    return await strategy.ExecuteAsync(async () =>
    {
        using (var transaction = await context.Database.BeginTransactionAsync())
        {
            try
            {
                // 1. Obtener la orden con detalles
                var existingOrder = await context.OrderRequest
                    .Include(o => o.OrderRequestDetails)
                    .FirstOrDefaultAsync(o => o.id == id);

                if (existingOrder == null)
                    return new ProcessResponseDto { IsSuccess = false, Mssg = "Orden no encontrada." };

                // 2. Devolver stock de items antiguos
                foreach (var detail in existingOrder.OrderRequestDetails)
                {
                    var item = await context.Items.FirstOrDefaultAsync(i => i.id == detail.item_id);
                    if (item != null)
                    {
                        item.stock += detail.total_units;
                        context.Items.Update(item); // marca como modificado
                    }
                }

                // 3. Eliminar detalles antiguos
                context.OrderRequestDetails.RemoveRange(existingOrder.OrderRequestDetails);

                // 4. Crear detalles nuevos y validar stock
                List<OrderRequestDetails> updatedDetails = new List<OrderRequestDetails>();

                foreach (var newDetail in orderDetail)
                {
                    var item = await context.Items.FirstOrDefaultAsync(i => i.id == newDetail.item_id);
                    if (item == null)
                        throw new Exception($"Item con ID {newDetail.item_id} no existe.");

                    if (item.stock < newDetail.total_units)
                        throw new Exception($"Stock insuficiente para el item {newDetail.item_name}.");

                    item.stock -= newDetail.total_units;
                    context.Items.Update(item);

                    var detail = new OrderRequestDetails
                    {
                        orderRequest_id = id,
                        item_id = newDetail.item_id,
                        item_name = newDetail.item_name,
                        discount_percent = newDetail.discount_percent,
                        discount_value = newDetail.discount_value,
                        subtotal_before_tax = newDetail.subtotal_before_tax,
                        total_units = newDetail.total_units,
                        price = newDetail.price,
                        iva = newDetail.iva,
                        iva_value = newDetail.iva_value,
                        igv = newDetail.igv,
                        igv_value = newDetail.igv_value,
                        totalIgv = newDetail.totalIgv,
                        totalIva = newDetail.totalIva
                    };

                    updatedDetails.Add(detail);
                }

                // 5. Actualizar totales en la orden
                existingOrder.baseTax = updatedDetails.Sum(x => x.subtotal_before_tax);
                existingOrder.descount = updatedDetails.Sum(x => x.discount_value ?? 0); // Revisa que la propiedad se llame así
                existingOrder.grossSubtotal = updatedDetails.Sum(x => x.subtotal_before_tax);
                existingOrder.valueIva = updatedDetails.Sum(x => x.iva_value ?? 0);
                existingOrder.valueIgv = updatedDetails.Sum(x => x.igv_value);
                existingOrder.totalIva = updatedDetails.Sum(x => x.totalIva);
                existingOrder.totalIgv = updatedDetails.Sum(x => x.totalIgv);

                context.OrderRequest.Update(existingOrder);

                // 6. Agregar detalles nuevos
                await context.OrderRequestDetails.AddRangeAsync(updatedDetails);

                // 7. Guardar cambios
                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ProcessResponseDto
                {
                    IsSuccess = true,
                    Mssg = "Orden actualizada correctamente."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ProcessResponseDto
                {
                    IsSuccess = false,
                    Mssg = $"Error en la actualización: {ex.Message}"
                };
            }
        }
    });
    }
    #endregion
    public Task<OrderResponseDto> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}