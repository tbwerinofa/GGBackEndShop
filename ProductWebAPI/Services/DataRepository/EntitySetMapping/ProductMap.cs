using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            modelBuilder.HasIndex(a => new { a.Name, a.Code,a.UserId }).IsUnique();
            modelBuilder.Property(a => a.Name).IsRequired();
            modelBuilder.Property(a => a.Code).IsRequired();
            modelBuilder.Property(a => a.Price).IsRequired();
            modelBuilder.Property(a => a.UserId).IsRequired();
;

        }
    }
}
