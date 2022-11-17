using Review.Infrastructure.Persistance.Models.ProductAggregateDBModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Review.API.Models.ProductAggregateDtos
{
    public class ProductDetailResponse
    {
        public ProductDetailResponse(ProductDetailResponseDto responseDto)
        {
            Id = responseDto.Id;
            Name = responseDto.Name;
            Description = responseDto.Description;
            Price = responseDto.Price;
            AverageScore = responseDto.AverageScore;
            RecommendationPercentage = responseDto.RecommendationPercentage;
            CreateTime = responseDto.CreateTime;
            Review = responseDto.Review?.Select(responseDtoReview => new ProductReviewResponse(responseDtoReview)).ToList();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public double AverageScore { get; set; }
        public int RecommendationPercentage { get; set; }
        public DateTime CreateTime { get; set; }
        public List<ProductReviewResponse> Review { get; set; }
    }
}
