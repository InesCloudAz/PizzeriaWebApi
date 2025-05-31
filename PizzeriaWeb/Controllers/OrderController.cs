using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizzeria.Core.Interfaces;
using System.Security.Claims;
using static Pizzeria.Core.DTO.OrderDTO;

namespace Pizzeria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        [Route("api/create-order")]
        public async Task<IActionResult> CreateOrder(AddOrderDTO orderDTO)
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (orderDTO == null)
                return BadRequest("You need to enter order");

            if (userId == null)
                return Unauthorized("Missing authentication");

            if (User.IsInRole("PremiumUser"))
            {

                await _orderService.AddPremiumOrder(orderDTO, userId);
                return Created();
            }
            else
            {

                await _orderService.AddOrder(orderDTO, userId);
                return Created();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAuthUserOrders()
        {

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("Missing authentication");

            var orders = await _orderService.GetAuthUserOrders(userId);
            return Ok(orders);
        }

        [HttpDelete("{orderId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {

            try
            {

                await _orderService.DeleteOrder(orderId);
                return Ok("Order deleted");
            }
            catch (ArgumentException Ex)
            {
                return NotFound(Ex.Message);
            }
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromQuery] bool delivered)
        {

            try
            {

                await _orderService.UpdateOrderStatus(orderId, delivered);
                return Ok("Order status updated");
            }
            catch (ArgumentException Ex)
            {

                return NotFound(Ex.Message);
            }
        }
    }
}
