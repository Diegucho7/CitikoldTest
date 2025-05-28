using RetailCitikold.Domain.Entities;

namespace RetailCitikold.Domain.Dtos.Response;


public partial record OrderRequestResponseDto(bool IsSuccess = true, string Error = "");

// public record OrderponseTotalDto(  List<OrderRequest> order = default);
public record OrderResponseTotalDto
{
    public List<OrderRequest> Orders { get; init; } = new();
    public List<Person> Persons { get; init; } = new();
}
// public record OrderRequestWithClientDto
// {
//     // Propiedades de OrderRequest
//     public int Id { get; init; }
//     public int? IdPaymentTime { get; init; }
//     public decimal? BaseNoObjectIva { get; init; }
//     public decimal? BaseExemptIva { get; init; }
//     public decimal? BaseIvaZero { get; init; }
//     public decimal? BaseTax { get; init; }
//     public decimal? Descount { get; init; }
//     public decimal? GrossSubtotal { get; init; }
//     public decimal? ValueIva { get; init; }
//     public decimal? ValueIce { get; init; }
//     public decimal? TotalIva { get; init; }
//     public decimal? TotalIgv { get; init; }
//     public decimal? ValueIgv { get; init; }
//     public int IdClient { get; init; }
//     public string? NumDocument { get; init; }
//
//     // Propiedad de Person
//     public string? NumberIdentification { get; init; }
//     public string? apellidoPaterno { get; set; }
//     public string? apellidoMaterno { get; set; }
//     public string? primerNombre { get; set; }
//     public string? segundoNombre { get; set; }
// }


public record OrderRequestWithClientDto
{
    // Propiedades de OrderRequest
    public int Id { get; init; }
    public int? IdPaymentTime { get; init; }
    public decimal? BaseNoObjectIva { get; init; }
    public decimal? BaseExemptIva { get; init; }
    public decimal? BaseIvaZero { get; init; }
    public decimal? BaseTax { get; init; }
    public decimal? Descount { get; init; }
    public decimal? GrossSubtotal { get; init; }
    public decimal? ValueIva { get; init; }
    public decimal? ValueIce { get; init; }
    public decimal? TotalIva { get; init; }
    public decimal? TotalIgv { get; init; }
    public decimal? ValueIgv { get; init; }
    public int IdClient { get; init; }
    public string? NumDocument { get; init; }

    public string? NumberIdentification { get; init; }
    public string? apellidoPaterno { get; set; }
    public string? apellidoMaterno { get; set; }
    public string? primerNombre { get; set; }
    public string? segundoNombre { get; set; }
}


