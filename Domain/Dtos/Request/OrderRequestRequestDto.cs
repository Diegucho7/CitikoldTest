using System.Text.Json.Serialization;

namespace RetailCitikold.Domain.Dtos.Request;

public record OrderRequestRequestDto (
    int id_user,
    int item_id,
    // int OrderRequest_id,
    string item_name,
    int total_units,
    decimal price,
    decimal discount_percent,
    decimal discount_value,
    decimal subtotal_before_iva,
    // bool Iva,
    // int iva_rate_id,
    // decimal iva_value,
    // decimal subtotal_before_igv,
    // bool igv,
    // int igv_rate_id,
    // decimal igv_value,
    // bool ice,
    // int ice_rate_id,
    // decimal ice_value,
    decimal net_subtotal
    
);

public record OrderRequestUtilRequestDto(
    
    // int id_user,
    int item_id,
    string item_name,
    int total_units,
    decimal price,
    decimal discount_percent,
    decimal discount_value,
    decimal subtotal_before_tax,
    decimal iva,
    decimal iva_value,
    decimal igv,
    decimal igv_value,
    decimal net_subtotal,
    decimal totalIva,
    decimal totalIgv,
    int id_state,
    decimal baseTax,
    decimal discount,
    decimal grossSubtotal,
    decimal valueIva,
    decimal valueIgv,
    decimal total,
    int id_client
);