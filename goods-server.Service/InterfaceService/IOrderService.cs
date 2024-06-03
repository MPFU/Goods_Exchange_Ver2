using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync(OrderDTO order);
        Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<bool> UpdateOrderAsync(Guid orderId, OrderDTO order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }

}
