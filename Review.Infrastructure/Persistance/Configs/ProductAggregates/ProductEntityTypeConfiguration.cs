using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.ProductAggregates;

namespace Review.Infrastructure.Persistance.Configs.ProductAggregates
{
    internal class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(i => i.Name).IsRequired(true);
            builder.Property(i => i.Name).HasMaxLength(100);

            builder.Ignore(i => i.AcceptedReviews);

            builder.Property(i => i.Description).IsRequired(true);

            builder.HasMany(i => i.Reviews).WithOne(i => i.Product).HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
