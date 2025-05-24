namespace RetailCitikold.Domain.Entities;

public class Establishment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int IdState { get; set; }
    public int IdDivision { get; set; }
    public int IdProvince { get; set; }
    public string Address { get; set; } = null!;
    public string Image { get; set; } = null!;
}