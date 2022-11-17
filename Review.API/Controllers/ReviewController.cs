using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Review.API.Models.Common;
using Review.API.Models.ProductAggregateDtos;
using Review.Application.DomainServices;
using Review.Infrastructure.Persistance.Models.Common;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IProductService _productService;

        public ReviewController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }
        /// <summary>
        /// submiting the review for one product (it's AllowAnonymout, but if we have authentication, this endpoint should have it, too)
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<string>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        public async Task<IActionResult> AddProductReview([FromBody] SubmitProductReviewRequest request, CancellationToken cancellationToken = default)
        {
            await _productService.AddProductReviewAsync(request.MapToProductView(), cancellationToken);
            return Ok("Your view is submitted successfully");
        }

        /// <summary>
        /// Getting the reviews of the product by pagination (to get allViews, set page and pageSize = 0)
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("OfProduct")]
        [ProducesResponseType(typeof(ApiResult<PaginationResponse<ProductReviewResponse>>), 200)]
        public async Task<PaginationResponse<ProductReviewResponse>> AddProductReview([FromQuery, Required] int productId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var productViews = await _productService.GetProductReviewByPaginationAsync(productId, page, pageSize, cancellationToken);
            var result = new PaginationResponse<ProductReviewResponse>()
            {
                Items = productViews.Items?.ConvertAll(product => new ProductReviewResponse(product)),
                TotalCount = productViews.TotalCount
            };
            return result;
        }

    }
}