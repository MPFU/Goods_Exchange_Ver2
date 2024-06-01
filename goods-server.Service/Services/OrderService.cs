using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.UtcNow;
            await _unitOfWork.OrderRepo.AddAsync(order);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        public async Task<bool> UpdateOrderAsync(Guid orderId, OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            return await _unitOfWork.OrderRepo.UpdateOrderAsync(orderId, order);
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await _unitOfWork.OrderRepo.DeleteOrderAsync(orderId);
        }
    }

}
