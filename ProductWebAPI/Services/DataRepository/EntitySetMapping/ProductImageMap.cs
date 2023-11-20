using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.DataRepository;

namespace ProductWebAPI.Services.DataRepository.EntitySetMapping
{
    public partial class ProductImageMap : InsightEntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
        }

        public override void Configure(EntityTypeBuilder<ProductImage> modelBuilder)
        {

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(b => b.CreatedTimestamp).HasDefaultValueSql("GetDate()");

            modelBuilder.HasIndex(a => new { a.ProductId, a.FileNameGuid }).IsUnique();
            modelBuilder.Property(a => a.ProductId).IsRequired();
            modelBuilder.Property(a => a.FileNameGuid).IsRequired();
            modelBuilder.Property(a => a.FileName).IsRequired();

            modelBuilder.HasOne(u => u.Product)
             .WithMany(u => u.ProductImages)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.HasOne(u => u.CreatedUser)
            // .WithMany(u => u.CreatedProductImages)
            // .IsRequired()
            // .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
