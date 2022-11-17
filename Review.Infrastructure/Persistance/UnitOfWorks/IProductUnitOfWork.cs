using Review.Domain.ProductAggregates;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.UnitOfWorks
{
    public interface IProductUnitOfWork
    {
        Task AddProductsRange(IEnumerable<Product> products, bool saveNow = true, CancellationToken cancellationToken = default);
        Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
        bool IsAnyProductExists();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}