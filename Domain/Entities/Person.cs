namespace RetailCitikold.Domain.Entities;

public class Person
{
    public int id { get; set; }
    public int idTypePerson { get; set; }
    public int idTypeTaxpayer { get; set; }
    public int idTypeIdentification { get; set; }
    public string numberIdentification { get; set; }
    public string companyName { get; set; }
    public string fechaCreacion { get; set; }
    public string fechaInicioOperaciones { get; set; }
    public int idEstado { get; set; }
    public string emailTax { get; set; }
    public string emailGeneral { get; set; }
    public int idGroupCompany { get; set; }
    public bool isSupplier { get; set; }
    public bool esClient { get; set; }
    public bool esEmployed { get; set; }
    public bool esAnother { get; set; }
    
    
    public string apellidoPaterno { get; set; }
    public string apellidoMaterno { get; set; }
    public string primerNombre { get; set; }
    public string segundoNombre { get; set; }
    public int idEstadoCivil { get; set; }
    public int idGenero { get; set; }
    public string fechaNacimiento { get; set; }
    public bool sufridoEmfermedadGrave { get; set; }
    public string enfermedadGrave { get; set; }
    public bool operado { get; set; }
    public string operadoDe { get; set; }
}