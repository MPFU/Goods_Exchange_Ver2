using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
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

        public async Task<PagedResult<GetOrderDTO>> GetAllOrdersAsync(OrderFilter orderFilter)
        {
            var orderList = _mapper.Map<IEnumerable<GetOrderDTO>>(await _unitOfWork.OrderRepo.GetAllAsync());
            IQueryable<GetOrderDTO> filterOrder = orderList.AsQueryable();

            // Filtering
            if (orderFilter.OrderId != null)
                filterOrder = filterOrder.Where(x => x.OrderId == orderFilter.OrderId);
            if (orderFilter.CustomerId != null)
                filterOrder = filterOrder.Where(x => x.CustomerId == orderFilter.CustomerId);
            if (orderFilter.OrderDate != null)
                filterOrder = filterOrder.Where(x => x.OrderDate == orderFilter.OrderDate);
            if (orderFilter.TotalPrice != null)
                filterOrder = filterOrder.Where(x => x.TotalPrice == orderFilter.TotalPrice);
            if (!string.IsNullOrEmpty(orderFilter.Status))
                filterOrder = filterOrder.Where(x => x.Status.Contains(orderFilter.Status, StringComparison.OrdinalIgnoreCase));

            // Sorting
            if (!string.IsNullOrEmpty(orderFilter.SortBy))
            {
                switch (orderFilter.SortBy.ToLower())
                {
                    case "orderdate":
                        filterOrder = orderFilter.SortAscending ?
                            filterOrder.OrderBy(o => o.OrderDate) :
                            filterOrder.OrderByDescending(o => o.OrderDate);
                        break;

                    case "totalprice":
                        filterOrder = orderFilter.SortAscending ?
                            filterOrder.OrderBy(o => o.TotalPrice) :
                            filterOrder.OrderByDescending(o => o.TotalPrice);
                        break;

                    default:
                        filterOrder = orderFilter.SortAscending ?
                            filterOrder.OrderBy(item => GetProperty.GetPropertyValue(item, orderFilter.SortBy)) :
                            filterOrder.OrderByDescending(item => GetProperty.GetPropertyValue(item, orderFilter.SortBy));
                        break;
                }
            }


            // Paging
            var pageItems = filterOrder
                .Skip((orderFilter.PageNumber - 1) * orderFilter.PageSize)
                .Take(orderFilter.PageSize)
                .ToList();

            return new PagedResult<GetOrderDTO>
            {
                Items = pageItems,
                PageNumber = orderFilter.PageNumber,
                PageSize = orderFilter.PageSize,
                TotalItem = filterOrder.Count(),
                TotalPages = (int)Math.Ceiling((decimal)filterOrder.Count() / orderFilter.PageSize)
            };
        }

        public async Task<GetOrderDTO> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _unitOfWork.OrderRepo.GetOrderByIdAsync(orderId);
            return _mapper.Map<GetOrderDTO>(order);
        }

    }

}
