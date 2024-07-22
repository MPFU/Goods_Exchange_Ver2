using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetails);
        }

        [HttpGet("{orderId}/{productId}")]
        public async Task<IActionResult> GetOrderDetailByOrderAndProductId(Guid orderId, Guid productId)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByOrderAndProductIdAsync(orderId, productId);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderDetailByOrderId(Guid orderId)
        {
            var orderDetails = await _orderDetailService.GetOrderDetailByOrderIdAsync(orderId);
            return Ok(orderDetails);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail([FromBody] CreateOrderDetailDTO createOrderDetailDTO)
        {
            var result = await _orderDetailService.CreateOrderDetailAsync(createOrderDetailDTO);
            if (result)
            {
                return Ok("OrderDetail created successfully.");
            }
            return BadRequest("Failed to create order detail.");
        }

        [HttpPut("{orderId}/{productId}")]
        public async Task<IActionResult> UpdateOrderDetail(Guid orderId, Guid productId, [FromBody] UpdateOrderDetailDTO updateOrderDetailDTO)
        {
            var result = await _orderDetailService.UpdateOrderDetailAsync(orderId, productId, updateOrderDetailDTO);
            if (result)
            {
                return Ok("OrderDetail updated successfully.");
            }
            return BadRequest("Failed to update order detail.");
        }

        [HttpDelete("{orderId}/{productId}")]
        public async Task<IActionResult> DeleteOrderDetail(Guid orderId, Guid productId)
        {
            var result = await _orderDetailService.DeleteOrderDetailAsync(orderId, productId);
            if (result)
            {
                return Ok("OrderDetail deleted successfully.");
            }
            return BadRequest("Failed to delete order detail.");
        }
    }
}
