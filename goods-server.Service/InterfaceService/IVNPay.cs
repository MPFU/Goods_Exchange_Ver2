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
        Task<string> CreatePaymentUrl(RequestVNPayDTO vNPayDTO);
        Task<VnPaymentResponseModel> PaymentExecute(IQueryCollection collections);
    }
}
