using Review.Domain.ProductAggregates;
using Review.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.UnitOfWorks
{
    public class ProductUnitOfWork : IProductUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ProductReporitory _productReporitory;

        public ProductUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _productReporitory = new(_dbContext);
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
    }
}
