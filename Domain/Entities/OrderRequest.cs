namespace RetailCitikold.Domain.Entities;

public class OrderRequest
{
    public int Id { get; set; }
    public int IdTypeOrderRequest { get; set; }
    public int IdPriority { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Reference { get; set; } = null!;
    public DateTime CancellationDate { get; set; }
    public string TradeObservation { get; set; } = null!;
    public bool DepositPayment { get; set; }
    public int IdEstablishment { get; set; }
    public int IdCompany { get; set; }
    public string BankObservation { get; set; } = null!;
    public int IdTypeDocument { get; set; }
}