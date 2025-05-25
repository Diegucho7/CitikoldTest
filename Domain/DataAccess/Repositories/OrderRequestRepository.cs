using System.Data;
using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Context;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Request;
using RetailCitikold.Domain.Dtos.Response;
using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class OrderRequestRepository(RetailCitikoldDbContext context) : IOrderRequestService
{
    #region Create Register

    public async Task<ProcessResponseDto> CreateOrder(OrderRequestRequestDto orderDetail)
    {

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {


                    var orden = await context.OrderRequest.FirstOrDefaultAsync(o => o.id_state == 3);
                    var taxItemsWithFees = await (
                        from tax in context.TaxItem
                        join fee in context.Fee on tax.id_fee equals fee.Id
                        where tax.id_item == orderDetail.item_id
                        select new
                        {
                            TaxItem = tax,
                            Fee = fee
                        }
                    ).ToListAsync();
                    var lastSecuencial = await context.Document
                        .Where(d => d.NumSerie == "001")
                        .OrderByDescending(d => d.Sequential)
                        .FirstOrDefaultAsync();

                    int siguienteSecuencial = (lastSecuencial?.Sequential ?? 0);



                    if (orden == null)
                    {


                        var document = new Document()
                        {
                            DateCreateAt = DateTime.Now.ToShortDateString(),
                            IssueDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt",
                                System.Globalization.DateTimeFormatInfo.InvariantInfo),
                            NumSerie = "001",
                            Sequential = (siguienteSecuencial) + 1,
                            NumDocument = $"001-001-{(siguienteSecuencial + 1).ToString("D9")}",
                            Reference = "ORDEN DE COMPRA",
                            id_userCreate = orderDetail.item_id,
                            id_typeDocument = 6,
                            AutomaticallyGenerated = true
                        };


                        var orderDocument = context.Document.Add(document);
                        await context.SaveChangesAsync();
                        var order = new OrderRequest()
                        {
                            Id = orderDocument.Entity.id ?? 1,
                            id_user = orderDetail.id_user,
                            id_state = 3,
                            Id_typeDocument = 6,
                            Id_company = 1,
                            Id_establishment = 1,
                            id_country = 1
                        };
                        var orderCreate = context.OrderRequest.Add(order);
                        await context.SaveChangesAsync();
                        int orderId = orderCreate.Entity.Id ?? 1;

                        var orderDetails = new OrderRequestDetails()
                        {

                            OrderRequest_id = orderId,
                            Item_id = orderDetail.item_id,
                            Item_name = orderDetail.item_name,
                            Total_units = orderDetail.total_units,
                            Price = orderDetail.price,
                            Discount_percent = orderDetail.discount_percent,
                            Discount_value = orderDetail.discount_value,
                            subtotal_before_iva = orderDetail.price * orderDetail.discount_percent,
                            Iva = (from imp in context.TaxItem
                                    join tipo in context.TypeTax on imp.id_typeTax equals tipo.Id
                                    join ta in context.Fee on imp.id_fee equals ta.Id
                                    where imp.id_item == orderDetail.item_id && imp.id_typeTax == 1
                                    select ta.PercentageEcu != 0
                                ).FirstOrDefault(),
                            Iva_rate_id = 0,
                            Iva_value = ((from imp in context.TaxItem
                                        join tipo in context.TypeTax
                                            on imp.id_typeTax equals tipo.Id
                                        join ta in context.Fee
                                            on imp.id_fee equals ta.Id
                                        where imp.id_item == orderDetail.item_id && imp.id_typeTax == 1
                                        select imp.id_fee == 1 ? ta.PercentageEcu : (decimal?)null
                                    ).FirstOrDefault() ?? 0m)
                                * orderDetail.net_subtotal / 100,
                            Subtotal_before_igv = orderDetail.price * orderDetail.discount_percent,
                            Igv = (from imp in context.TaxItem
                                    join tipo in context.TypeTax on imp.id_typeTax equals tipo.Id
                                    join ta in context.Fee on imp.id_fee equals ta.Id
                                    where imp.id_item == orderDetail.item_id && imp.id_typeTax == 1
                                    select ta.PercentagePe != 0
                                ).FirstOrDefault(),
                            Igv_rate_id = 0,
                            Igv_value = ((from imp in context.TaxItem
                                        join tipo in context.TypeTax
                                            on imp.id_typeTax equals tipo.Id
                                        join ta in context.Fee
                                            on imp.id_fee equals ta.Id
                                        where imp.id_item == orderDetail.item_id && imp.id_typeTax == 7
                                        select imp.id_fee == 7 ? ta.PercentagePe : (decimal?)null
                                    ).FirstOrDefault() ?? 0m)
                                * orderDetail.net_subtotal / 100,
                            Ice = (from imp in context.TaxItem
                                    join tipo in context.TypeTax on imp.id_typeTax equals tipo.Id
                                    join ta in context.Fee on imp.id_fee equals ta.Id
                                    where imp.id_item == orderDetail.item_id && imp.id_typeTax == 2
                                    select ta.PercentagePe != 0
                                ).FirstOrDefault(),
                            Ice_rate_id = 0,
                            Ice_value = ((from imp in context.TaxItem
                                        join tipo in context.TypeTax
                                            on imp.id_typeTax equals tipo.Id
                                        join ta in context.Fee
                                            on imp.id_fee equals ta.Id
                                        where imp.id_item == orderDetail.item_id && imp.id_typeTax == 2
                                        select imp.id_fee == 2 ? ta.PercentagePe : (decimal?)null
                                    ).FirstOrDefault() ?? 0m)
                                * orderDetail.net_subtotal / 100,
                            Net_subtotal = orderDetail.net_subtotal,
                            Observation = "Sin observaciones"
                        };

                        context.OrderRequestDetails.Add(orderDetails);

                        await context.SaveChangesAsync();

                        return new ProcessResponseDto
                        {
                            IsSuccess = false,
                            Mssg = "Ok"
                        };
                    }   

                    return new ProcessResponseDto
                    {
                        IsSuccess = false,
                        Mssg = "Ok"
                    };
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    
                    return new ProcessResponseDto
                    {
                        IsSuccess = false,
                        Mssg = ex.Message
                    };
                }



            }
        });
        // if (orden != null)
        // {
        //     var detail = await context.OrderRequestDetails.
        return new ProcessResponseDto
        {
            IsSuccess = false,
            Mssg = "ex.Message"
        };
        //         Where(d => d.OrderRequest_id == orden.Id).ToListAsync();
        //     if (detail.Count == 0)
        //     {
        //         return new ProcessResponseDto
        //         {
        //             IsSuccess = false,
        //             Mssg = "Tiene una orden de compra pendiente sin articulos"
        //         };
        //     }
        //     var subtotales = await (from o in context.OrderRequestDetails
        //         join it in context.Items on o.Item_id equals it.id
        //         join t in context.TaxItem on it.id equals t.IdItem
        //         where o.OrderRequest_id == orden.Id
        //         group o by 1 into g
        //         select new
        //         {
        //             subNoIva =g.Sum(x => x.subtotal_before_iva),
        //             SubTotalEcua = g.Sum(x => x.subtotal_before_iva),
        //             SubTotalPeru = g.Sum(x => x.Subtotal_before_igv),
        //             TotalDiscount = g.Sum(x => x.Discount_value),
        //             TotalIva = g.Sum(x => x.Iva_value),
        //             TotalIGV = g.Sum(x => x.Igv_value),
        //             TotalIce = g.Sum(x => x.Ice_value),
        //             Total = g.Sum(x => x.Net_subtotal)
        //         }).FirstOrDefaultAsync();
        //     
        //     var pendiente = new OrderRequest()
        //     {
        //
        //         baseObjetoNoIva = subtotales?.SubTotalEcua,
        //         baseExentoIva = subtotales?.subNoIva,
        //         baseIvaCero = subtotales?.subNoIva,
        //         baseGravaIva = subtotales?.TotalIva,
        //         baseImponible = subtotales?.SubTotalEcua,
        //         descuento = subtotales?.TotalDiscount,
        //         subTotalBruto = subtotales?.Total,
        //         valorIva = subtotales?.TotalIva,
        //         valueIgv = subtotales?.TotalIGV,
        //         valorIce = subtotales?.TotalIce,
        //         total = subtotales?.Total
        //     };
        //     
        //     
        //
        //
        //     _ =  await context.SaveChangesAsync();
        //     await transaction.CommitAsync();
        // }
        // var dtoDetails  = new OrderRequestDetails()
        // {
        //     OrderRequest_id     = orderDetail.OrderRequest_id,
        //     Item_id             = orderDetail.item_id,
        //     ItemName            = orderDetail.item_name,
        //     Total_units         = orderDetail.total_units,
        //     Price               = orderDetail.price,
        //     Discount_percent    = orderDetail.discount_percent,
        //     Discount_value      = orderDetail.discount_value,
        //     subtotal_before_iva = orderDetail.subtotal_before_iva,
        //     Iva                 = orderDetail.Iva,
        //     Iva_rate_id         = orderDetail.iva_rate_id,
        //     Iva_value           = orderDetail.iva_value,
        //     Subtotal_before_igv = orderDetail.subtotal_before_igv,
        //     Igv                 = orderDetail.igv,
        //     Igv_rate_id         = orderDetail.igv_rate_id,
        //     Igv_value           = orderDetail.igv_value,
        //     Ice                 = orderDetail.ice,
        //     Ice_rate_id         = orderDetail.ice_rate_id,
        //     Ice_value           = orderDetail.ice_value,
        //     Net_subtotal        = orderDetail.net_subtotal,
        // };




        //     
        //         await transaction.CommitAsync();
        //     return new ProcessResponseDto
        //     {
        //         IsSuccess = true,
        //         Mssg = ""
        //     };
        // }
        // catch (Exception ex)
        // {
        //     await transaction.RollbackAsync(); // Deshacer todo si algo falla
        //     return new ProcessResponseDto
        //     {
        //         IsSuccess = false,
        //         Mssg = $"Error al guardar la Orden de compra: {ex.InnerException?.Message ?? ex.Message}"
        //     };
        // }

    }

#endregion

    public Task<OrderResponseDto> ReadOrder(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProcessResponseDto> UpdateOrder(OrderRequestRequestDto order)
    {
        throw new NotImplementedException();
    }

    public Task<OrderResponseDto> DeleteOrder(int id)
    {
        throw new NotImplementedException();
    }
}