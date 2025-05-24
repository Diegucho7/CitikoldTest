namespace RetailCitikold.Domain.Entities;

public class TypeDocument
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Alias { get; set; } = null!;
    public bool Tax { get; set; }
    public string CodeSri { get; set; } = null!;
    public string CodeSunat { get; set; } = null!;
    public int IdState { get; set; }
    public bool ReserveSistem { get; set; }
    public string CodeControl { get; set; } = null!;    
    public bool IsIn { get; set; }
    public bool IsCredit { get; set; }
    public bool IsPaid { get; set; }
    public bool IsGeneratesLogisticCost { get; set; }
}