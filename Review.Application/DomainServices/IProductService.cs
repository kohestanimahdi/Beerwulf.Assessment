using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.Models.Common;
using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Application.DomainServices
{
    public interface IProductService
    {
        Task AddProductReviewAsync(ProductReview productReview, CancellationToken cancellationToken = default);
        Task<ProductDetailResponseDto> GetProductDetailsAsync(int productId, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductReview>> GetProductReviewByPaginationAsync(int productId, int page, int pageSize, CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductListItemDto>> GetProductsAsListItemAsync(int page, int pageSize, CancellationToken cancellationToken = default);
    }
}