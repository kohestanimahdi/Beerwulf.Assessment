using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using System;

namespace Review.API.Models.ProductAggregateDtos
{
    public class ProductListItemResponse
    {
        public ProductListItemResponse(ProductListItemDto itemDto)
        {
            Id = itemDto.Id;
            Name = itemDto.Name;
            Description = itemDto.Description;
            Price = itemDto.Price;
            AverageScore = Math.Round(itemDto.AverageScore, 2);
            RecommendationPercentage = itemDto.RecommendationPercentage;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public double AverageScore { get; set; }
        public int RecommendationPercentage { get; set; }
    }
}
