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
                    segundoNombre = person.segundoNombre
                }).FirstOrDefaultAsync();

           

          
            return result; //
        }
        catch (Exception)
        {
           
            return null;
        }
    
    }

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
                    NumberIdentification = person.numberIdentification
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
    public Task<ProcessResponseDto> UpdateOrder(OrderRequestRequestDto order)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}