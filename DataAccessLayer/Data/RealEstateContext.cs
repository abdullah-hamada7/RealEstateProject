using DataAccessLayer.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data;

public class RealEstateContext : DbContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
   

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<DomainModels.Type> Types { get; set; }
    //public virtual DbSet<ContactUs> ContactUs { get; set; }

    
}
