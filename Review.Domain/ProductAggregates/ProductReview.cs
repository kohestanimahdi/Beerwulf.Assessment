using Review.Domain.Common;
using Review.Domain.ProductAggregates.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Review.Domain.ProductAggregates
{
    public class ProductReview : BaseEntity
    {
        [Range(1, 5)]
        public byte Score { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public bool IsRecommended { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }

        //! this is set to accepted by default, but it should set to pending and stay in this status until the admin accept this review or reject it
        // onlu accepted reviews show in the website
        public ProductReviewStatuses Status { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
