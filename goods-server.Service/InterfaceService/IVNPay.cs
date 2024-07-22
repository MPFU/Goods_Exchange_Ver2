using goods_server.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IVNPay
    {
        public string CreatePaymentUrl(RequestVNPayDTO vNPayDTO);
        public Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
