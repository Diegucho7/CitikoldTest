namespace RetailCitikold.Domain.Entities;

public class State
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Description { get; set; } = null!;
}