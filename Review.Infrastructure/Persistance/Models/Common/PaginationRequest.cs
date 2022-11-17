using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Review.Infrastructure.Persistance.Models.Common
{
    public class PaginationRequest
    {
        public PaginationRequest(int page, int pageSize)
        {
            if (page < 0)
                throw new System.ArgumentOutOfRangeException(nameof(page));

            if (pageSize < 0)
                throw new System.ArgumentOutOfRangeException(nameof(pageSize));

            PageSize = pageSize;
            Page = page;
        }


        public int PageSize { get; set; }
        public int Page { get; set; }

        public int SkipCount => (Page - 1) * PageSize;

        public bool IsNeedAllItems() => Page == 0 && 0 == PageSize;
    }

    public static class PaginationExtensions
    {
        public static async Task<PaginationResponse<T>> GetAsPaginationAsync<T>(this IQueryable<T> query, PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
        {
            var totalCount = await query.CountAsync(cancellationToken);
            if (!paginationRequest.IsNeedAllItems())
                query = query.Skip(paginationRequest.SkipCount).Take(paginationRequest.PageSize);

            var items = await query.ToListAsync(cancellationToken);

            return new PaginationResponse<T>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

    }
}
