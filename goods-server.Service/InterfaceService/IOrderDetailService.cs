using goods_server.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailDTO>> GetAllOrderDetailsAsync();
        Task<OrderDetailDTO?> GetOrderDetailByOrderAndProductIdAsync(Guid orderId, Guid productId);
        Task<bool> CreateOrderDetailAsync(CreateOrderDetailDTO orderDetail);
        Task<bool> UpdateOrderDetailAsync(Guid orderId, Guid productId, UpdateOrderDetailDTO orderDetail);
        Task<bool> DeleteOrderDetailAsync(Guid orderId, Guid productId);
    }
}
