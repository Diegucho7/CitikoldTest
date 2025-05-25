namespace RetailCitikold.Domain.Entities;

public class OrderRequest
{
    public int? Id { get; set; }
    public int? Id_typeOrderRequest { get; set; }
    public int? Id_priority { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string? Reference { get; set; } = null!;
    public DateTime CancellationDate { get; set; }
    public string? TradeObservation { get; set; } = null!;
    public bool? DepositPayment { get; set; }
    public int? Id_establishment { get; set; }
    public int? Id_company { get; set; }
    public int? id_country { get; set; }
    public int? Id_typeDocument { get; set; }
    public int? id_state { get; set; }
    public int? id_user { get; set; }
    public int? id_paymentMethod{ get; set; }
    public int? id_paymentTime{ get; set; }
    public decimal? baseNoObjectIva{ get; set; }
    public decimal? baseExemptIva{ get; set; }
    public decimal? baseIvaZero{ get; set; }
    public decimal? baseTax{ get; set; }
    public decimal? descount{ get; set; }
    public decimal? grossSubtotal{ get; set; }
    public decimal? valueIva{ get; set; }
    public decimal? valueIgv{ get; set; }
    public decimal? valueIce{ get; set; }
    public decimal? total{ get; set; }
}