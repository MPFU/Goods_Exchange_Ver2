using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.FilterModel;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class RequestHistoryService : IRequestHistoyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RequestHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateRequestHistoryAsync(RequestHistoryDTO requestHistoryDto)
        {
            var requestHistory = _mapper.Map<RequestHistory>(requestHistoryDto);
            requestHistory.CreatedDate = DateTime.UtcNow;
            requestHistory.Id = Guid.NewGuid(); // Tạo một Guid mới
            await _unitOfWork.RequestHistoryRepo.AddAsync(requestHistory);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }


      
        public async Task<bool> UpdateRequestHistoryAsync(Guid requestHistoryId, UpdateRequestHistoryDTO requestHistoryDto)
        {
            var existingRequestHistory = await _unitOfWork.RequestHistoryRepo.GetRequestHistoryByIdAsync(requestHistoryId);
            if (existingRequestHistory == null)
            {
                return false;
            }

            existingRequestHistory.BuyerId = requestHistoryDto.BuyerId; // Cập nhật BuyerId
            existingRequestHistory.SellerId = requestHistoryDto.SellerId; // Cập nhật SellerId
            existingRequestHistory.ProductSellerId = requestHistoryDto.ProductSellerId; // Cập nhật ProductSellerId
            existingRequestHistory.ProductBuyerId = requestHistoryDto.ProductBuyerId; // Cập nhật ProductBuyerId
            existingRequestHistory.Status = requestHistoryDto.Status; // Cập nhật Status
            _unitOfWork.RequestHistoryRepo.Update(existingRequestHistory);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }



        public async Task<bool> DeleteRequestHistoryAsync(Guid requestId)
        {
            return await _unitOfWork.RequestHistoryRepo.DeleteRequestHistoryAsync(requestId);
        }

        public async Task<PagedResult<GetRequestHistoryDTO>> GetAllRequestHistoriesAsync(RequestHistoryFilter filter)
        {
            var requestHistoryList = _mapper.Map<IEnumerable<GetRequestHistoryDTO>>(await _unitOfWork.RequestHistoryRepo.GetAllRequestHistories());
            IQueryable<GetRequestHistoryDTO> filterRequestHistory = requestHistoryList.AsQueryable();

            // Filtering
            if (filter.BuyerId.HasValue)
                filterRequestHistory = filterRequestHistory.Where(rh => rh.BuyerId == filter.BuyerId);
            if (filter.SellerId.HasValue)
                filterRequestHistory = filterRequestHistory.Where(rh => rh.SellerId == filter.SellerId);
            if (filter.ProductSellerId.HasValue)
                filterRequestHistory = filterRequestHistory.Where(rh => rh.ProductSellerId == filter.ProductSellerId);
            if (filter.ProductBuyerId.HasValue)
                filterRequestHistory = filterRequestHistory.Where(rh => rh.ProductBuyerId == filter.ProductBuyerId);
         

            // Paging
            var pageItems = filterRequestHistory
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PagedResult<GetRequestHistoryDTO>
            {
                Items = pageItems,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItem = filterRequestHistory.Count(),
                TotalPages = (int)Math.Ceiling((decimal)filterRequestHistory.Count() / filter.PageSize)
            };
        }
        public async Task<GetRequestHistoryDTO> GetRequestHistoryByIdAsync(Guid requestHistoryId)
        {
            var requestHistory = await _unitOfWork.RequestHistoryRepo.GetRequestHistoryByIdAsync(requestHistoryId);
            return _mapper.Map<GetRequestHistoryDTO>(requestHistory);
        }

        public async Task<PagedResult<GetRequestHistoryDTO>> GetRequestHistoriesByAccountIdAsync(RequestHistoryFilter filter)
        {
            var requestHistoryList = _mapper.Map<IEnumerable<GetRequestHistoryDTO>>(await _unitOfWork.RequestHistoryRepo.GetRequestHistoriesByAccountIdAsync(filter.AccountId));
            IQueryable<GetRequestHistoryDTO> filterRequestHistory = requestHistoryList.AsQueryable();

            // Paging
            var pageItems = filterRequestHistory
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PagedResult<GetRequestHistoryDTO>
            {
                Items = pageItems,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItem = requestHistoryList.Count(),
                TotalPages = (int)Math.Ceiling((decimal)requestHistoryList.Count() / filter.PageSize)
            };
        }

        public async Task<bool> UpdateStatusAsync(Guid requestHistoryId, UpdateRequestHistoryStatusDTO statusDto)
        {
            var requestHistory = await _unitOfWork.RequestHistoryRepo.GetByIdAsync(requestHistoryId);
            if (requestHistory == null)
            {
                return false;
            }

            requestHistory.Status = statusDto.Status;
            _unitOfWork.RequestHistoryRepo.Update(requestHistory);
            await _unitOfWork.SaveAsync();

            return true;
        }

    }


}
