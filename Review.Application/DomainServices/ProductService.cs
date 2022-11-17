using Review.Domain.Exceptions;
using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.Models.Common;
using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using Review.Infrastructure.Persistance.UnitOfWorks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Application.DomainServices
{
    public class ProductService : IProductService
    {
        private readonly IProductUnitOfWork _productUnitOfWork;

        public ProductService(IProductUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork ?? throw new ArgumentNullException(nameof(productUnitOfWork));
        }

        public async Task AddProductReviewAsync(ProductReview productReview, CancellationToken cancellationToken = default)
        {
            // TODO we can add a rate limmiter here to prevent flooding requests for adding review 

            var product = await _productUnitOfWork.GetProductByIdAsync(productReview.ProductId, cancellationToken)
                                ?? throw new NotFoundException("The product is not found");

            product.AddReview(productReview);
            await _productUnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public Task<PaginationResponse<ProductListItemDto>> GetProductsAsListItemAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            //TODO We can add cache in this like to prevent request to the database for getting the list of the products => the key of the cache => ($"ProductsAsItem-{page}-{pageSize}")

            var paginationRequest = new PaginationRequest(page, pageSize);
            return _productUnitOfWork.GetProductsAsListItemAsync(paginationRequest, cancellationToken);
        }

        public Task<PaginationResponse<ProductReview>> GetProductReviewByPaginationAsync(int productId, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            var paginationRequest = new PaginationRequest(page, pageSize);
            return _productUnitOfWork.GetProductReviewsByPaginationAsync(productId, paginationRequest, cancellationToken);
        }
    }
}
