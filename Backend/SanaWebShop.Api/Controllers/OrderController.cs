using Microsoft.AspNetCore.Mvc;
using System.Net;
using SanaWebShop.Api.Business.Interfaces;
using SanaWebShop.Api.Business.DTOs.Categories;
using SanaWebShop.Api.Business.DTOs;
using SanaWebShop.Api.Business.DTOs.Products;
using SanaWebShop.Api.Business.Implements;

namespace SanaWebShop.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IActionsOrdersBusiness _bus;

        public OrderController(IActionsOrdersBusiness bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Method to create a Shopping Cart.
        /// </summary>
        /// <param name="preOrder"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateShoppingCart([FromBody] PreOrderDto preOrder)
        {
            try
            {
                var res = await _bus.CreateShoppingCart(preOrder);
                var response = new ApiResponse<string>(res)
                {
                    IsSuccess = res == "ok"
                };
                return response.IsSuccess ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>("Error found: " + ex.Message)
                {
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Search all products of Order customer.
        /// </summary>
        /// <param name="req">Object that content the customer and order code.</param>
        /// <returns>When search is successfully, List of Products information and Ok are returned, 
        /// otherwise BadRequest are returned.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<ShoppingCartDtoResponse>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetShoppingCart([FromQuery] OrderUserDto req)
        {
            try
            {
                var result = await _bus.GetShoppingCart(req);
                var response = new ApiResponse<List<ShoppingCartDtoResponse>>(result)
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

        /// <summary>
        /// Method to Delete From Shopping Cart.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ok if successfully, else error description.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteFromShoppingCart(int id)
        {
            try
            {
                var res = await _bus.DeleteFromShoppingCart(id);
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
        /// Change the quantity of Product from Shopping Cart.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>ok if successfully, else error description.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateQuantity([FromBody] ShoppingCartDto data)
        {
            try
            {
                var res = await _bus.EditQuantity(data);
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
        /// Method to process the order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> ProcessOrder(int id)
        {
            try
            {
                var res = await _bus.ProcessOrder(id);
                var response = new ApiResponse<string>(res)
                {
                    IsSuccess = res == "ok"
                };
                return response.IsSuccess ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>("Error found: " + ex.Message)
                {
                    IsSuccess = false
                };
                return BadRequest(response);
            }
        }

    }
}