using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDetailDTO>> GetAllOrderDetailsAsync()
        {
            var orderDetails = await _unitOfWork.OrderDetailRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDetailDTO>>(orderDetails);
        }

        public async Task<OrderDetailDTO?> GetOrderDetailByOrderAndProductIdAsync(Guid orderId, Guid productId)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepo.GetByOrderAndProductIdAsync(orderId, productId);
            return _mapper.Map<OrderDetailDTO>(orderDetail);
        }

        public async Task<bool> CreateOrderDetailAsync(CreateOrderDetailDTO orderDetail)
        {
            var newOrderDetail = _mapper.Map<OrderDetail>(orderDetail);
            await _unitOfWork.OrderDetailRepo.AddAsync(newOrderDetail);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateOrderDetailAsync(Guid orderId, Guid productId, UpdateOrderDetailDTO orderDetail)
        {
            var existingOrderDetail = await _unitOfWork.OrderDetailRepo.GetByOrderAndProductIdAsync(orderId, productId);
            if (existingOrderDetail == null)
            {
                return false;
            }

            _mapper.Map(orderDetail, existingOrderDetail);
            _unitOfWork.OrderDetailRepo.Update(existingOrderDetail);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteOrderDetailAsync(Guid orderId, Guid productId)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepo.GetByOrderAndProductIdAsync(orderId, productId);
            if (orderDetail == null)
            {
                return false;
            }

            _unitOfWork.OrderDetailRepo.Delete(orderDetail);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
