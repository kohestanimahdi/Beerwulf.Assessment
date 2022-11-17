using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Review.API.Models.Common;
using Review.API.Models.ProductAggregateDtos;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
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
            return Ok("Your view is submitted successfully");
        }
    }
}