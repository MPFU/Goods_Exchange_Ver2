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


        public async Task<PagedResult<GetRequestHistory2DTO>> GetAllRequestHistoriesAsync(RequestHistoryFilter Requestfilter)
        {
            var requestHistoryList = _mapper.Map<IEnumerable<GetRequestHistory2DTO>>(await _unitOfWork.RequestHistoryRepo.GetAllRequestHistories());
            IQueryable<GetRequestHistory2DTO> filterRequestHistory = requestHistoryList.AsQueryable();

            // Filtering
            if (Requestfilter.BuyerId != null)
                filterRequestHistory = filterRequestHistory.Where(x => x.BuyerId.Equals(Requestfilter.BuyerId));
            if (Requestfilter.SellerId != null)
                filterRequestHistory = filterRequestHistory.Where(x => x.SellerId.Equals(Requestfilter.SellerId));
            if (Requestfilter.ProductSellerId != null)
                filterRequestHistory = filterRequestHistory.Where(x => x.ProductSellerId.Equals(Requestfilter.ProductSellerId));
            if (Requestfilter.ProductBuyerId != null)
                filterRequestHistory = filterRequestHistory.Where(x => x.ProductBuyerId.Equals(Requestfilter.ProductBuyerId));
            if (!string.IsNullOrEmpty(Requestfilter.Status))
                filterRequestHistory = filterRequestHistory.Where(x => x.Status.Contains(Requestfilter.Status, StringComparison.OrdinalIgnoreCase));

            // Sorting
            if (!string.IsNullOrEmpty(Requestfilter.SortBy))
            {
                switch (Requestfilter.SortBy)
                {
                    case "buyerId":
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(rh => rh.BuyerId) :
                            filterRequestHistory.OrderByDescending(rh => rh.BuyerId);
                        break;

                    case "sellerId":
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(rh => rh.SellerId) :
                            filterRequestHistory.OrderByDescending(rh => rh.SellerId);
                        break;

                    case "productSellerId":
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(rh => rh.ProductSellerId) :
                            filterRequestHistory.OrderByDescending(rh => rh.ProductSellerId);
                        break;

                    case "productBuyerId":
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(rh => rh.ProductBuyerId) :
                            filterRequestHistory.OrderByDescending(rh => rh.ProductBuyerId);
                        break;

                    case "status":
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(rh => rh.Status) :
                            filterRequestHistory.OrderByDescending(rh => rh.Status);
                        break;

                    default:
                        filterRequestHistory = Requestfilter.SortAscending ?
                            filterRequestHistory.OrderBy(item => GetProperty.GetPropertyValue(item, Requestfilter.SortBy)) :
                            filterRequestHistory.OrderByDescending(item => GetProperty.GetPropertyValue(item, Requestfilter.SortBy));
                        break;
                }
            }

            // Paging
            var pageItems = filterRequestHistory
                .Skip((Requestfilter.PageNumber - 1) * Requestfilter.PageSize)
                .Take(Requestfilter.PageSize)
                .ToList();

            return new PagedResult<GetRequestHistory2DTO>
            {
                Items = pageItems,
                PageNumber = Requestfilter.PageNumber,
                PageSize = Requestfilter.PageSize,
                TotalItem = filterRequestHistory.Count(),
                TotalPages = (int)Math.Ceiling((decimal)filterRequestHistory.Count() / Requestfilter.PageSize)
            };
        }


        public async Task<GetRequestHistoryDTO> GetRequestHistoryByIdAsync(Guid requestHistoryId)
        {
            var requestHistory = await _unitOfWork.RequestHistoryRepo.GetRequestHistoryByIdAsync(requestHistoryId);
            return _mapper.Map<GetRequestHistoryDTO>(requestHistory);
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
