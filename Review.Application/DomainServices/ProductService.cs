using Review.Domain.Exceptions;
using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
