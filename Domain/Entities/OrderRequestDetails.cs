namespace RetailCitikold.Domain.Entities;

public class OrderRequestDetails
{
    public int Id { get; set; }
    public int OrderRequest_id { get; set; }
    public int Item_id { get; set; }
    public string Item_name { get; set; } = null!;
    public decimal Total_units { get; set; }
    public decimal Price { get; set; }
    public decimal Discount_percent { get; set; }
    public decimal Discount_value { get; set; }
    public decimal subtotal_before_iva { get; set; }
    public bool Iva { get; set; }
    public int Iva_rate_id { get; set; }
    public decimal Iva_value { get; set; }
    public decimal Subtotal_before_igv { get; set; }
    public bool Igv { get; set; }
    public int Igv_rate_id { get; set; }
    public decimal Igv_value { get; set; }
    public bool Ice { get; set; }
    public int Ice_rate_id { get; set; }
    public decimal Ice_value { get; set; }
    public decimal Net_subtotal { get; set; }
    public string Observation { get; set; } = null!;
}