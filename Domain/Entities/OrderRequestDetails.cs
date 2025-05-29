namespace RetailCitikold.Domain.Entities;

public class OrderRequestDetails
{
    public int id { get; set; }
    public int orderRequest_id { get; set; }
    public int item_id { get; set; }
    public string? item_name { get; set; } = null!;
    public int total_units { get; set; }
    public decimal price { get; set; }
    public decimal? discount_percent { get; set; }
    public decimal ?discount_value { get; set; }
    public decimal subtotal_before_tax { get; set; }
    public decimal iva { get; set; }
    public int? iva_rate_id { get; set; }
    public decimal? iva_value { get; set; }
    public decimal igv { get; set; }
    public int? igv_rate_id { get; set; }
    public decimal igv_value { get; set; }
    public bool? ice { get; set; }
    public int? ice_rate_id { get; set; }
    public decimal? ice_value { get; set; }
    public decimal? net_subtotal { get; set; }
    public decimal totalIva { get; set; }
    public decimal totalIgv { get; set; }
    
    public virtual OrderRequest OrderRequest { get; set; }
    public string? observation { get; set; }
}