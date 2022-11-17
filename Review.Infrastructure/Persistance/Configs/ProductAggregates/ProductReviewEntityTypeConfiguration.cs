using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.ProductAggregates;

namespace Review.Infrastructure.Persistance.Configs.ProductAggregates
{
    internal class ProductReviewEntityTypeConfiguration : IEntityTypeConfiguration<ProductReview>
    {
        public void Configure(EntityTypeBuilder<ProductReview> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Comment).IsRequired();
            builder.Property(i => i.Title).IsRequired();
        }
    }
}
