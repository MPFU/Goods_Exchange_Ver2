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

        public async Task<Guid?> CreateOrderAsync(OrderDTO orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.OrderId = Guid.NewGuid(); // Tạo một Guid mới
            order.OrderDate = DateTime.UtcNow;
            await _unitOfWork.OrderRepo.AddAsync(order);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result ? order.OrderId : null;
        }





        public async Task<IEnumerable<GetOrderDTO>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            var orders = await _unitOfWork.OrderRepo.GetOrdersByCustomerIdAsync(customerId);
            return _mapper.Map<IEnumerable<GetOrderDTO>>(orders);
        }

        public async Task<bool> UpdateOrderAsync(Guid orderId, UpdateOrderDTO orderDto)
        {
            var existingOrder = await _unitOfWork.OrderRepo.GetOrderByIdAsync(orderId);
            if (existingOrder == null)
            {
                return false;
            }

            existingOrder.TotalPrice = orderDto.TotalPrice; // Cập nhật TotalPrice
            existingOrder.Status = orderDto.Status;
            _unitOfWork.OrderRepo.Update(existingOrder);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }

        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, UpdateOrder2DTO order)
        {
            return await _unitOfWork.OrderRepo.UpdateStatusAsync(orderId, order.Status);
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            return await _unitOfWork.OrderRepo.DeleteOrderAsync(orderId);
        }

        public async Task<IEnumerable<GetOrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<GetOrderDTO>>(orders);
        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepo.GetOrderByIdAsync(orderId);
            return _mapper.Map<GetOrderDTO>(order);
        }

    }

}
