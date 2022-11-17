using Review.Domain.ProductAggregates;
using System;
using System.Collections.Generic;

namespace Review.Infrastructure.Persistance.Models.ProductAggregateDBModels
{
    public class ProductDetailResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public double AverageScore { get; set; }
        public int RecommendationPercentage { get; set; }
        public DateTime CreateTime { get; set; }
        public IEnumerable<ProductReview> Review { get; set; }
    }
}
