using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IOrderDetailRepo : IGenericRepo<OrderDetail>
    {
        Task<OrderDetail?> GetByOrderAndProductIdAsync(Guid orderId, Guid productId);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid OrderID);
    }
}
