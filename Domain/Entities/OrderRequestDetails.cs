namespace RetailCitikold.Domain.Entities;

public class OrderRequestDetails
{
    public int Id { get; set; }
    public int OrderRequestId { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; } = null!;
    public decimal TotalUnits { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal SubtotalBeforeIva { get; set; }
    public decimal Iva { get; set; }
    public int IvaRateId { get; set; }
    public decimal IvaValue { get; set; }
    public decimal SubtotalBeforeIgv { get; set; }
    public bool Igv { get; set; }
    public int IgvRateId { get; set; }
    public decimal IgvValue { get; set; }
    public bool Ice { get; set; }
    public int IceRateId { get; set; }
    public decimal IceValue { get; set; }
    public decimal NetSubtotal { get; set; }
    public string Observation { get; set; } = null!;
}