using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.Entities;


namespace RetailCitikold.Domain.DataAccess.Context;

public class RetailCitikoldDbContext : DbContext
{
    public RetailCitikoldDbContext(DbContextOptions<RetailCitikoldDbContext> options) : base(options)
    {
          
    }
    #region DbSet
    
    public DbSet<Users> Users { get; set; }
    public DbSet<Person> Person { get; set; }
    public DbSet<Items> Items { get; set; }
    public DbSet<Document> Document { get; set; }
    public DbSet<Establishment> Establishment { get; set; }
    public DbSet<Fee> Fee { get; set; }
    public DbSet<Inventary> Inventary { get; set; }
    public DbSet<OrderRequest> OrderRequest { get; set; }
    public DbSet<OrderRequestDetails> OrderRequestDetails { get; set; }
    public DbSet<ProductType> ProductType { get; set; }
    public DbSet<SalesDocument> SalesDocument { get; set; }
    public DbSet<SalesDocumentDetails> SalesDocumentDetails { get; set; }
    public DbSet<State> State { get; set; }
    public DbSet<Stock> Stock { get; set; }
    public DbSet<TaxItem> TaxItem { get; set; }
    public DbSet<TypeDocument> TypeDocument { get; set; }
    public DbSet<TypeTax> TypeTax { get; set; }
    
    public DbSet<Country> Country { get; set; }
    public DbSet<SourceDocument> SourceDocument { get; set; }
   
    #endregion
    
    
}