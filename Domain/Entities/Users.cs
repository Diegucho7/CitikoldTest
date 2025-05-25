namespace RetailCitikold.Domain.Entities
{
    public class Users
    {
        public int id { get; set; } =  0;
        public string username { get; set; } 
        public string password { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public int? id_Group { get; set; }
        public int? id_Estado { get; set; }
        public string? image { get; set; }
        public int? id_Person { get; set; }
        public int attempts { get; set; }
    }
}
