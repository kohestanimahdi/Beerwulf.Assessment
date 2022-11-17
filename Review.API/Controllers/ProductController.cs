using Microsoft.AspNetCore.Mvc;
using Review.API.Configuration.Filters;
using Review.API.Models.Common;
using Review.API.Models.ProductAggregateDtos;
using Review.Application.DomainServices;
using Review.Domain.Exceptions;
using Review.Infrastructure.Persistance.Models.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetProductsAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetProductsAsListItemAsync(page, pageSize, cancellationToken);
            var result = new PaginationResponse<ProductListItemResponse>()
            {
                Items = products.Items?.ConvertAll(product => new ProductListItemResponse(product)),
                TotalCount = products.TotalCount
            };

            return Ok(result);
        }

        /// <summary>
        /// get the product and its reviews
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ApiResult<ProductDetailResponse>), 200)]
        public async Task<IActionResult> GetProductDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _productService.GetProductDetailsAsync(id, cancellationToken);
            if (product is null)
                throw new NotFoundException("Product is not found");

            var result = new ProductDetailResponse(product);
            return Ok(result);
        }

    }
}
