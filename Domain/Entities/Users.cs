namespace RetailCitikold.Domain.Entities
{
    public class Users
    {
        public int id { get; set; } =  0;
        public string username { get; set; } 
        public string password { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public int? id_Group { get; set; } = 1;
        public int? id_Estado { get; set; } = 1;
        public string? image { get; set; }
        public int? id_Person { get; set; }
        public int attempts { get; set; }
        
        public string name { get; set; }
        
        public string lastName { get; set; }
        public string role { get; set; }
    }
}
