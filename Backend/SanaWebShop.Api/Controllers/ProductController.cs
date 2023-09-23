using Microsoft.AspNetCore.Mvc;
using System.Net;
using SanaWebShop.Api.Business.Interfaces;
using SanaWebShop.Api.Business.DTOs.Categories;
using SanaWebShop.Api.Business.DTOs;
using SanaWebShop.Api.Business.DTOs.Products;

namespace SanaWebShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IActionsProductsBusiness _bus;

        public ProductController(IActionsProductsBusiness bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Method to create a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto category)
        {
            try
            {
                var res = await _bus.AddCategory(category);
                var response = new ApiResponse<string>(res)
                {
                    IsSuccess = res == "ok"
                };
                return response.IsSuccess ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>("Error found: " + ex.InnerException?.Message)
                {
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Method to create a product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddProduct([FromBody] ProductInfoDto product)
        {
            try
            {
                var res = await _bus.AddProduct(product);
                var response = new ApiResponse<string>(res)
                {
                    IsSuccess = res == "ok"
                };
                return response.IsSuccess ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>("Error found: " + ex.InnerException?.Message)
                {
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Search all products that match the filters.
        /// </summary>
        /// <remarks>
        /// Parameters description:
        /// 
        ///      pageNumber => It is the actual page number of the table.
        ///      pageSize => It is the maximum number of items per page
        /// </remarks>
        /// <param name="req">Object that content the filters for print on tables.</param>
        /// <returns>When search is successfully, List of Products information and Ok are returned, 
        /// otherwise BadRequest are returned.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ProductDetailResponseDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsDetail([FromQuery] InfoTableDto req)
        {
            try
            {
                var result = await _bus.GetProductsDetail(req);
                var response = new ApiResponse<ProductDetailResponseDto>(result)
                {
                    IsSuccess = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>("Error found: " + ex.InnerException?.Message)
                {
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }
    }
}