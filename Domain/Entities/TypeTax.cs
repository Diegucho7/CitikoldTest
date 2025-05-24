namespace RetailCitikold.Domain.Entities;

public class TypeTax
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string CodeSri { get; set; } = null!;
    public string CodeSunat { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
}