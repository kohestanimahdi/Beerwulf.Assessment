using Review.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace Review.Domain.ProductAggregates
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }

        public List<ProductReview> Reviews { get; set; }

        public void AddReview(ProductReview review)
        {
            Reviews ??= new List<ProductReview>();
            Reviews.Add(review);
        }
        public IEnumerable<ProductReview> AcceptedReviews => Reviews?.Where(p => p.Status == ProductReviewStatuses.Accepted);

        public double GetRecommendationPercentage()
        {
            if (AcceptedReviews?.Any() == true)
                return AcceptedReviews.Count(review => review.IsRecommended) / AcceptedReviews.Count();

            return 0d;
        }

        public double GetAverageScore()
        {
            return AcceptedReviews?.Average(review => review.Score) ?? 0;
        }
    }
}
