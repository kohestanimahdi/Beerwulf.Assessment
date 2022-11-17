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
    }
}
