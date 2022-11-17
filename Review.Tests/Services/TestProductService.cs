using Moq;
using Review.Application.DomainServices;
using Review.Domain.Exceptions;
using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.UnitOfWorks;
using Review.Tests.DataProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Review.Tests.Services
{
    public class TestProductService
    {
        private readonly Mock<IProductUnitOfWork> _mockProductUnitOfWork;
        private readonly IProductService _productService;
        public TestProductService()
        {
            _mockProductUnitOfWork = new Mock<IProductUnitOfWork>();
            _productService = new ProductService(_mockProductUnitOfWork.Object);
        }

        [Fact]
        public void Test_AddProductReview_NotFoundProductException()
        {
            _mockProductUnitOfWork.Setup(i => i.GetProductByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(default(Product));
            var productView = new ProductReview
            {
                CreateTime = DateTime.Now,
                IsRecommended = false,
                ProductId = 1,
                Score = 1,
                Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                UserEmail = "kohestanimahdi@gmail.com",
                Comment = "test",
                Title = "test",
                UserFullName = "Mahdi Kouhestani"
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await _productService.AddProductReviewAsync(productView, CancellationToken.None));
        }

        [Fact]
        public async Task Test_AddProductReview_Success()
        {
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "test",
                Id = 1,
                Name = "test",
                Price = 1,
            };

            _mockProductUnitOfWork.Setup(i => i.GetProductByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(product);
            var productView = new ProductReview
            {
                CreateTime = DateTime.Now,
                IsRecommended = false,
                ProductId = 1,
                Score = 1,
                Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                UserEmail = "kohestanimahdi@gmail.com",
                Comment = "test",
                Title = "test",
                UserFullName = "Mahdi Kouhestani"
            };

            await _productService.AddProductReviewAsync(productView, CancellationToken.None);

            Assert.Contains(productView.Id, product.Reviews.Select(i => i.Id));
        }

        [Fact]
        public void Test_GetProductsAsListItemAsync_InvalidPageNumber()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _productService.GetProductsAsListItemAsync(-1, 0, CancellationToken.None));
        }

        [Fact]
        public void Test_GetProductsAsListItemAsync_InvalidPageSize()
        {
            Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _productService.GetProductsAsListItemAsync(1, -1, CancellationToken.None));
        }

        [Fact]
        public async Task Test_GetProductsAsListItemAsync_Success()
        {
            using var dbContext = new TestApplicationDbContext("GetProductsAsListItemAsync_Success");
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 1
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productUnitOfWork = new ProductUnitOfWork(dbContext);
            var productService = new ProductService(productUnitOfWork);

            var result = await productService.GetProductsAsListItemAsync(1, 20, CancellationToken.None);
            Assert.NotEmpty(result.Items);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task Test_GetProductsAsListItemAsync_SuccessAverageScore()
        {
            using var dbContext = new TestApplicationDbContext("GetProductsAsListItemAsync_SuccessAverageScore");
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 1,
                Reviews = new List<ProductReview>
                {
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 1,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    },
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 5,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    }
                }
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productUnitOfWork = new ProductUnitOfWork(dbContext);
            var productService = new ProductService(productUnitOfWork);

            var result = await productService.GetProductsAsListItemAsync(1, 20, CancellationToken.None);
            Assert.NotEmpty(result.Items);
            Assert.Equal(1, result.TotalCount);
            Assert.Equal(3, result.Items.First().AverageScore);
            Assert.Equal(100, result.Items.First().RecommendationPercentage);
        }

        [Fact]
        public async Task Test_GetProductReviewByPaginationAsync_InvalidProductId()
        {
            using var dbContext = new TestApplicationDbContext("GetProductReviewByPaginationAsync_InvalidProductId");
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 1,
                Reviews = new List<ProductReview>
                {
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 1,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    },
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 5,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    }
                }
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productUnitOfWork = new ProductUnitOfWork(dbContext);
            var productService = new ProductService(productUnitOfWork);

            var result = await productService.GetProductReviewByPaginationAsync(100, 1, 20, CancellationToken.None);
            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
        }

        [Fact]
        public async Task Test_GetProductReviewByPaginationAsync_Success()
        {
            using var dbContext = new TestApplicationDbContext("GetProductReviewByPaginationAsync_Success");
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 1,
                Id = 1,
                Reviews = new List<ProductReview>
                {
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 1,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    },
                    new ProductReview
                    {
                        Comment= "Test",
                        CreateTime = DateTime.Now,
                        IsRecommended= true,
                        Score= 5,
                        Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted,
                        Title = "Test",
                    }
                }
            };
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            var productUnitOfWork = new ProductUnitOfWork(dbContext);
            var productService = new ProductService(productUnitOfWork);

            var result = await productService.GetProductReviewByPaginationAsync(1, 1, 1, CancellationToken.None);
            Assert.Single(result.Items);
            Assert.Equal(2, result.TotalCount);
        }
    }
}
