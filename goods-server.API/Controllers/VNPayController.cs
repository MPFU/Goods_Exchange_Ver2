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

        public VNPayController( IVNPay vN)
        {
            vNPay = vN;
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
        public IActionResult VNPayReturn(Guid orderID, [FromQuery] VNPayReturnQuery query)
        {
            // Validate the return query parameters and handle the payment result
            // You can add more logic here to process the payment result

            
            return Ok(new SucceededResponseModel() 
            { 
                Message = "Payment successful", 
                Data = new { query , orderID }
            });
        }

    }
}
