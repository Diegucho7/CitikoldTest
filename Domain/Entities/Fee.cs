namespace RetailCitikold.Domain.Entities;

public class Fee
{
    public int Id { get; set; }
    public int? Id_TypeTax { get; set; }
    public string? CodeSri { get; set; } = null!;
    public string? CodeSunat { get; set; } = null!;
    public string? SystemCode { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public decimal? PercentageEcu { get; set; }
    public decimal? PercentagePe { get; set; }
}