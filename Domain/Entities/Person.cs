namespace RetailCitikold.Domain.Entities;

public class Person
{
    public int id { get; set; }
    public int? id_typePerson { get; set; }
    public int? id_typeTaxpayer { get; set; }
    public int? id_typeIdentification { get; set; }
    public string? numberIdentification { get; set; }
    public string? fechaCreacion { get; set; }
    public string? fechaInicioOperaciones { get; set; }
    public int? id_estado { get; set; }
    public string? emailTax { get; set; }
    public string? emailGeneral { get; set; }
    public int? id_GroupCompany { get; set; }
    public bool? isSupplier { get; set; }
    public bool? esClient { get; set; }
    public bool? esEmployed { get; set; }
    public bool? esAnother { get; set; }
    
    
    public string? apellidoPaterno { get; set; }
    public string? apellidoMaterno { get; set; }
    public string? primerNombre { get; set; }
    public string? segundoNombre { get; set; }
    public int? id_estadoCivil { get; set; }
    public int? id_genero { get; set; }
    public string? fechaNacimiento { get; set; }
    public bool? sufridoEmfermedadGrave { get; set; }
    public string? enfermedadGrave { get; set; }
    public bool? operado { get; set; }
    public string? operadoDe { get; set; }
}