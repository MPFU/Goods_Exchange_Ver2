using goods_server.Contracts;
using goods_server.Service.FilterModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IOrderService
    {
        Task<Guid?> CreateOrderAsync(OrderDTO order);
        Task<IEnumerable<GetOrderDTO>> GetOrdersByCustomerIdAsync(Guid customerId);
        Task<PagedResult<GetOrderDTO>> GetAllOrdersAsync(OrderFilter orderFilter);
        Task<bool> UpdateOrderAsync(Guid orderId, UpdateOrderDTO order);
        Task<bool> UpdateOrderStatusAsync(Guid orderId, UpdateOrder2DTO order);
        Task<bool> DeleteOrderAsync(Guid orderId);
        Task<GetOrderDTO> GetOrderByIdAsync(Guid orderId);

    }

}
