using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.Repositories
{
    internal class ProductReporitory
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductReporitory(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public bool IsAnyExists()
        => _dbContext.Products.Any();
    }
}
