using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.UnitOfWorks;
using System.Collections.Generic;

namespace Review.Infrastructure.Persistance.DataInitializer
{
    public class ProductDataInitializer : IDataInitializer
    {
        private readonly IProductUnitOfWork _productUnitOfWork;

        public ProductDataInitializer(IProductUnitOfWork productUnitOfWork)
        {
            _productUnitOfWork = productUnitOfWork;
        }

        public void InitializeData()
        {
            if (_productUnitOfWork.IsAnyProductExists())
                return;

            var products = GetSampleProducts();
            _productUnitOfWork.AddProductsRange(products);
        }

        private static IEnumerable<Product> GetSampleProducts()
        {
            for (int i = 1; i <= 10; i++)
            {
                yield return new Product
                {
                    CreateTime = System.DateTime.Now,
                    Description = $"The description of product {i}",
                    Name = $"The name of product {i}",
                    Price = (uint)i * 10,
                    Reviews = new()
                    {
                        new ProductReview
                        {
                            Score = 3,
                            Comment = $"Comment of the product {i}",
                            Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                            CreateTime = System.DateTime.Now,
                            IsRecommended = i%2 == 0,
                            Title = $"Title of the review for product {i}",
                            UserEmail = "kohestanimahdi@gmail.com",
                            UserFullName = "Mahdi Kouhestani"
                        }
                    }
                };
            }
        }
    }
}
