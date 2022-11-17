using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.UnitOfWorks
{
    public interface IProductUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}