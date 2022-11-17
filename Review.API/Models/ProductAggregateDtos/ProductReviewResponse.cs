using Review.Domain.ProductAggregates;
using System;

namespace Review.API.Models.ProductAggregateDtos
{
    public class ProductReviewResponse
    {
        public ProductReviewResponse(ProductReview review)
        {
            Id = review.Id;
            Score = review.Score;
            Title = review.Title;
            Comment = review.Comment;
            IsRecommended = review.IsRecommended;
            UserFullName = review.UserFullName;
            UserEmail = review.UserEmail;
            CreateTime = review.CreateTime;
        }

        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public byte Score { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
    }
}
