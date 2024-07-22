using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class VNPayService : IVNPay
    {
        public async Task<string> CreatePaymentUrl( RequestVNPayDTO vNPayDTO)
        {
            IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
            
            var tick = DateTime.Now.Ticks.ToString();

            var vnpay = new VnPayLibrary();
            vnpay.AddRequestData("vnp_Version", _config["Vnpay:Version"]);
            vnpay.AddRequestData("vnp_Command", _config["Vnpay:Command"]);
            vnpay.AddRequestData("vnp_TmnCode", _config["Vnpay:TmnCode"]);
            vnpay.AddRequestData("vnp_Amount", (vNPayDTO.TotalPrice * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", _config["Vnpay:CurrCode"]);
            vnpay.AddRequestData("vnp_IpAddr", "192.168.1.16");
            vnpay.AddRequestData("vnp_Locale", _config["Vnpay:Locale"]);
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + tick);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

            vnpay.AddRequestData("vnp_ReturnUrl", vNPayDTO.ReturnUrl);
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddHours(1).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_TxnRef", tick);

            string paymentUrl = vnpay.CreateRequestUrl(_config["Vnpay:BaseUrl"], _config["Vnpay:HashSecret"]);
            return paymentUrl;
        }

        public async Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections)
        {
            IConfiguration _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();

            var vnpay = new VnPayLibrary();

            foreach (var (key,value) in collections)
            {
                //get all querystring data
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;

            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["Vnpay:HashSecret"]);
            if (!checkSignature)
            {
                return new VnPaymentResponseModel()
                {
                    Success = false
                };
            }
            return new VnPaymentResponseModel
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderId = orderId.ToString(),
                TransactionId = vnpayTranId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode,
            };
        }
    }
}
