using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using goods_server.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly IVNPay vNPay;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;

        public VNPayController( IVNPay vN, IOrderService orderService, IOrderDetailService orderDetailService, IProductService productService)
        {
            vNPay = vN;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _productService = productService;

        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] RequestVNPayDTO request)
        {
            var paymentUrl = await vNPay.CreatePaymentUrl(request);
            return Ok(new SucceededResponseModel (){
                Status = Ok().StatusCode,
                Data = paymentUrl 
            });
        }

        [HttpGet("orderID={orderID}")]
        public async Task<IActionResult> VNPayReturn(Guid orderID, [FromQuery] VNPayReturnQuery query)
        {
            // Validate the return query parameters and handle the payment result
            // You can add more logic here to process the payment result

            if (query.vnp_ResponseCode.Equals("00"))
            {
                UpdateOrder2DTO updateOrder2DTO = new UpdateOrder2DTO();
                updateOrder2DTO.Status = "Success";
                var check = await _orderService.UpdateOrderStatusAsync(orderID, updateOrder2DTO);
                if (check)
                {
                   var orderLi = await _orderDetailService.GetOrderDetailByOrderIdAsync(orderID);
                    if(orderLi != null)
                    {
                        foreach(var item in orderLi)
                        {
                            UpdateQuantityProductDTO quantityProductDTO = new UpdateQuantityProductDTO();
                            quantityProductDTO.Quantity = item.Quantity;
                            await _productService.UpdateQuantityProduct(item.ProductId, quantityProductDTO);
                        }
                        return Redirect("http://localhost:3000/order/success");
                    }
                }
                else
                {
                    return BadRequest(new FailedResponseModel
                    {
                        Message = "Update Order Status Fail!"
                    });
                }
                
            }
            else
            {
                UpdateOrder2DTO updateOrder2DTO = new UpdateOrder2DTO();
                updateOrder2DTO.Status = "Fail";
                var check = await _orderService.UpdateOrderStatusAsync(orderID, updateOrder2DTO);
                if (check)
                {
                    return Redirect("http://localhost:3000/order/fail");
                }
                else
                {
                    return BadRequest(new FailedResponseModel
                    {
                        Message = "Update Order Status Fail!"
                    });
                }
               
            }
            return Ok(new SucceededResponseModel()
            {
                Message = "Payment successful",
                Data = new { query, orderID }
            });
        }

    }
}
