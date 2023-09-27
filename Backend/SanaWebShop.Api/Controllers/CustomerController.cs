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
    public class CustomerController : ControllerBase
    {
        private readonly IActionsCustomersBusiness _bus;

        public CustomerController(IActionsCustomersBusiness bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// Method to create a Customer.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Id from Customer created.</returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<int>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromQuery] string name)
        {
            try
            {
                var res = await _bus.Create(name);
                var response = new ApiResponse<int>(res)
                {
                    IsSuccess = res > 0
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
    }
}