using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailCitikold.Domain.Entities
{
    public class Items
    {
        public int id { get; set; }
        public string? code { get; set; } = null!;
        public string name { get; set; } = null!;
        public int? id_State { get; set; } 
        public decimal price { get; set; } 
        
        public byte[]? Image { get; set; }
        public string? description { get; set; } = null!;
        public int? id_ProductType { get; set; }
        public int? id_Brand { get; set; }
        public int? id_CountryOrigin { get; set; }
        public int stock { get; set; }
    }
}
