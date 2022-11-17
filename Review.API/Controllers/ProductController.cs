using Microsoft.AspNetCore.Mvc;
using Review.API.Configuration.Filters;
using Review.API.Models.Common;
using Review.Application.DomainServices;
using System.Threading.Tasks;
using System.Threading;
using System;
using Review.Infrastructure.Persistance.Models.Common;
using Review.API.Models.ProductAggregateDtos;

namespace Review.API.Controllers
{
    [ApiController]
    [ApiResultFilter]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        /// <summary>
        /// get the list of the product with its summary in pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<PaginationResponse<ProductListItemResponse>>), 200)]
        public async Task<PaginationResponse<ProductListItemResponse>> GetProductsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetProductsAsListItemAsync(page, pageSize, cancellationToken);
            var result = new PaginationResponse<ProductListItemResponse>()
            {
                Items = products.Items?.ConvertAll(product => new ProductListItemResponse(product)),
                TotalCount = products.TotalCount
            };

            return result;
        }
    }
}
