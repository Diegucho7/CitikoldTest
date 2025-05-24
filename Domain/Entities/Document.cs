namespace RetailCitikold.Domain.Entities
{
    public class Document
    {
        public int? id { get; set; }
        public int? id_pointIssue { get; set; }
        public int? id_typeDocument { get; set; }
        public int? id_sourceDocument { get; set; }
        public string? DateCreateAt { get; set; }
        public string? IssueDate { get; set; }
        public DateTime? IssueDateDT { get; set; }
        public string? NumSerie { get; set; }
        public int? Sequential { get; set; }
        public string? NumDocument { get; set; }
        public string? Reference { get; set; }
        public string? observation { get; set; }
        public bool? AutomaticallyGenerated { get; set; }
        public int? id_state { get; set; }
        public int? id_userCreate { get; set; }
        public int? id_userModify { get; set; }
        public int? id_userAnnular { get; set; }
 
    }
}
