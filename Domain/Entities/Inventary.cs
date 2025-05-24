namespace RetailCitikold.Domain.Entities;

public class Inventary
{
    public int Id { get; set; }
    public int IdPurchaseOrder { get; set; }
    public int IdPerson { get; set; }
    public string NumberDocument { get; set; } = null!;
    public decimal Total { get; set; }
}