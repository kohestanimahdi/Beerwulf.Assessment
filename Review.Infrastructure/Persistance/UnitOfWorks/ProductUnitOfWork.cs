using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.Models.Common;
using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using Review.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.UnitOfWorks
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ProductReporitory _productReporitory;
        private readonly ProductReviewReporitory _productReviewReporitory;

        public ProductUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _productReporitory = new(_dbContext);
            _productReviewReporitory = new(_dbContext);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);

        public bool IsAnyProductExists()
            => _productReporitory.IsAnyExists();

        public Task AddProductsRange(IEnumerable<Product> products, bool saveNow = true, CancellationToken cancellationToken = default)
        {
            _productReporitory.AddRange(products);
            if (saveNow)
                return SaveChangesAsync(cancellationToken);

            return Task.CompletedTask;
        }

        public Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken = default) =>
            _productReporitory.GetByIdAsync(id, cancellationToken);

        public Task<PaginationResponse<ProductListItemDto>> GetProductsAsListItemAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            var products = _productReporitory.GetProductsAsListItem();
            return products.GetAsPaginationAsync(paginationRequest, cancellationToken);
        }

        public Task<PaginationResponse<ProductReview>> GetProductReviewsByPaginationAsync(int productId, PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            var products = _productReviewReporitory.GetReviewsOfProduct(productId);
            return products.GetAsPaginationAsync(paginationRequest, cancellationToken);
        }

        public Task<ProductDetailResponseDto> GetProductDetailsAsync(int productId, CancellationToken cancellationToken = default)
            => _productReporitory.GetDetailsByIdAsync(productId, cancellationToken);
    }
}
