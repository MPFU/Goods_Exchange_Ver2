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
        Task<IEnumerable<GetOrderDTO>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<IEnumerable<GetOrderDTO>> GetAllOrdersAsync();
        Task<bool> UpdateOrderAsync(Guid orderId, UpdateOrderDTO order);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<GetOrderDTO> GetOrderByIdAsync(Guid orderId);

    }

}
