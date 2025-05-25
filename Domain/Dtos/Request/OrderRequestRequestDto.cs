namespace RetailCitikold.Domain.Dtos.Request;

public record OrderRequestRequestDto (
    int id_user,
    int item_id,
    // int OrderRequest_id,
    string item_name,
    decimal total_units,
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