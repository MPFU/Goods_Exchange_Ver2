using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Core.InterfacesRepo
{
    public interface IOrderRepo : IGenericRepo<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<bool> UpdateOrderAsync(Guid orderId, Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<Order> GetOrderByIdAsync(Guid orderId);

    }

}
