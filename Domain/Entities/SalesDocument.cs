namespace RetailCitikold.Domain.Entities;

public class SalesDocument

{
    public int Id { get; set; }
    public bool GoodsOrServices { get; set; }
    public DateTime DeliveryDate { get; set; }
    public DateTime DueDate { get; set; }
    public int ClientId { get; set; }
    public int ClientAddressId { get; set; }
    public string BillingAddress { get; set; } = null!;
    public int SellerId { get; set; }
    public int PaymentMethodId { get; set; }
    public int PaymentChannelId { get; set; }
    public int ReasonId { get; set; }
    public decimal NonIvaBase { get; set; }
    public decimal IvaExemptBase { get; set; }
    public decimal ZeroIvaBase { get; set; }
    public decimal IvaBase { get; set; }
    public decimal NonIgvBase { get; set; }
    public decimal IgvExemptBase { get; set; }
    public decimal ZeroIgvBase { get; set; }
    public decimal IgvBase { get; set; }
    public decimal TaxBase { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalIce { get; set; }
    public decimal TotalIva { get; set; }
    public decimal TotalIgv { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TotalCollected { get; set; }
    public decimal TotalToCollect { get; set; }
    public decimal TotalCrossed { get; set; }
    public bool WaitingForWithholding { get; set; }
    public bool ZeroBalanceInvoice { get; set; }
    public string SourceReference { get; set; } = null!;
    public bool ExternalOrigin { get; set; }
    public bool IsImportation { get; set; }
    public int IdTypeDocument { get; set; }
}
