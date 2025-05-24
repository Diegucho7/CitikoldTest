namespace RetailCitikold.Domain.Entities;

public class Stock
{
    public int Id { get; set; }
    public int IdStore { get; set; }
    public int IdItem { get; set; }
    public int IdLocation { get; set; }
    public int IdBatch { get; set; }
    public decimal Inventory { get; set; }
    public decimal AveragePrice { get; set; }
    public decimal Cost { get; set; }
}