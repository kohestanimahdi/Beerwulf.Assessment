using Review.Domain.ProductAggregates;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Application.DomainServices
{
    public interface IProductService
    {
        Task AddProductReviewAsync(ProductReview productReview, CancellationToken cancellationToken = default);
    }
}