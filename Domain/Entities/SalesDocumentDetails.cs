namespace RetailCitikold.Domain.Entities;

public class SalesDocumentDetails
{
    public int Id { get; set; }
    public int IdSalesDocument { get; set; }
    public int ItemId { get; set; }
    public string ItemName { get; set; } = null!;
    public int UnitId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TaxableBase { get; set; }
    public decimal GrossSubtotal { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal IceValue { get; set; }
    public decimal IvaValue { get; set; }
    public decimal IgvValue { get; set; }
    public decimal NetSubtotal { get; set; }
    public string Observation { get; set; } = null!;
    public int CostCenterId { get; set; }
    public int ReferenceDetailId { get; set; }
}