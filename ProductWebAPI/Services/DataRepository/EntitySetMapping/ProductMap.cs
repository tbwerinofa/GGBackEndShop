using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;

namespace ProductWebAPI.DataRepository
{
    public partial class ProductMap : InsightEntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
        }

        public override void Configure(EntityTypeBuilder<Product> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            //modelBuilder.HasIndex(a => new { a.Name, a.ProductCategoryId }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Code).IsRequired();
            modelBuilder.Property(a => a.Price).IsRequired();

           // modelBuilder.HasOne(u => u.UpdatedUser)
           //.WithMany(u => u.UpdatedProducts)
           //.OnDelete(DeleteBehavior.Restrict);

        }
    }
}
