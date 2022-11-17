using Review.Domain.ProductAggregates.Enums;
using Review.Domain.ProductAggregates;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Review.Infrastructure.Persistance.Repositories
{
    internal class ProductReviewReporitory
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductReviewReporitory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IQueryable<ProductReview> GetReviewsOfProduct(int productId)
           => _dbContext.ProductReviews.Where(i => i.ProductId == productId && i.Status == ProductReviewStatuses.Accepted)
                .OrderByDescending(i => i.CreateTime).AsNoTracking();
    }
}
