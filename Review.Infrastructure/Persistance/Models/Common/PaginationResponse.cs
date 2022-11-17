using System.Collections.Generic;

namespace Review.Infrastructure.Persistance.Models.Common
{
    public class PaginationResponse<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
