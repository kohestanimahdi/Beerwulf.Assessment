using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.UnitOfWorks;
using Review.Tests.DataProvider;
using System;
using Xunit;

namespace Review.Tests.UnitOfWorks
{
    public class TestProductUnitOfWork : IDisposable
    {
        private readonly IProductUnitOfWork _productUnitOfWork;
        private readonly TestApplicationDbContext _dbContext;
        public TestProductUnitOfWork()
        {
            _dbContext = new TestApplicationDbContext("TestProductUnitOfWork");
            _productUnitOfWork = new ProductUnitOfWork(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public void Test_IsAnyProductExists_ReturnsFalse()
        {
            var result = _productUnitOfWork.IsAnyProductExists();
            Assert.False(result);
        }

        [Fact]
        public void Test_IsAnyProductExists_ReturnsTrue()
        {
            var product = new Product
            {
                CreateTime = DateTime.Now,
                Description = "Test",
                Name = "Test",
                Price = 1
            };
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
            var result = _productUnitOfWork.IsAnyProductExists();
            Assert.True(result);

            _dbContext.Products.RemoveRange(_dbContext.Products);
            _dbContext.SaveChanges();
        }
    }
}
