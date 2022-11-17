using Review.Domain.ProductAggregates;
using System.ComponentModel.DataAnnotations;

namespace Review.API.Models.ProductAggregateDtos
{
    public class SubmitProductReviewRequest
    {
        [Range(1, 5, ErrorMessage = "Score should be in range of 1 to 5")]
        public byte Score { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Comment is required")]
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public string UserFullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string UserEmail { get; set; }

        [Required(ErrorMessage = "ProductId is required")]
        public int ProductId { get; set; }

        public ProductReview MapToProductView() =>
            new()
            {
                Title = Title,
                Score = Score,
                Comment = Comment,
                ProductId = ProductId,
                IsRecommended = IsRecommended,
                UserFullName = UserFullName,
                UserEmail = UserEmail,
                CreateTime = System.DateTime.Now,

                //! this is set to accepted by default, but it should set to pending and stay in this status until the admin accept this review or reject it
                Status = Domain.ProductAggregates.Enums.ProductReviewStatuses.Accepted
            };
    }
}
