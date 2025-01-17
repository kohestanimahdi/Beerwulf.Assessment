﻿using Microsoft.EntityFrameworkCore;
using Review.Domain.ProductAggregates;
using Review.Domain.ProductAggregates.Enums;
using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.Repositories
{
    internal class ProductReporitory
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductReporitory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool IsAnyExists()
            => _dbContext.Products.Any();

        public void AddRange(IEnumerable<Product> products)
            => _dbContext.Products.AddRange(products);

        public Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
             => _dbContext.Products.FindAsync(new object[] { id }, cancellationToken).AsTask();

        public Task<ProductDetailResponseDto> GetDetailsByIdAsync(int productId, CancellationToken cancellationToken = default)
            => _dbContext.Products
            .Include(i => i.Reviews).Where(i => i.Id == productId)
            .Select(product => new ProductDetailResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                AverageScore = product.Reviews.Any() ? product.Reviews.Where(p => p.Status == ProductReviewStatuses.Accepted).Average(review => review.Score) : 0,
                RecommendationPercentage = product.Reviews.Any(p => p.Status == ProductReviewStatuses.Accepted) ?
                                        (int)(product.Reviews.Count(review => review.IsRecommended && review.Status == ProductReviewStatuses.Accepted) * 1f /
                                    product.Reviews.Count(p => p.Status == ProductReviewStatuses.Accepted) * 1f * 100) : 0,
                CreateTime = product.CreateTime,
                Review = product.Reviews != null ? product.Reviews.Where(p => p.Status == ProductReviewStatuses.Accepted).OrderByDescending(i => i.CreateTime).Take(20) : null
            }).FirstOrDefaultAsync(cancellationToken);


        public IQueryable<ProductListItemDto> GetProductsAsListItem()
            => _dbContext.Products
            .Include(i => i.Reviews)
            .Select(product => new ProductListItemDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                AverageScore = product.Reviews.Any() ? product.Reviews.Where(p => p.Status == ProductReviewStatuses.Accepted).Average(review => review.Score) : 0,
                RecommendationPercentage = product.Reviews.Any(p => p.Status == ProductReviewStatuses.Accepted) ?
                                        (int)(product.Reviews.Count(review => review.IsRecommended && review.Status == ProductReviewStatuses.Accepted) * 1f /
                                    product.Reviews.Count(p => p.Status == ProductReviewStatuses.Accepted) * 1f * 100) : 0
            }).AsNoTracking();
    }
}
