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
            await _unitOfWork.RequestHistoryRepo.AddAsync(requestHistory);
            var result = await _unitOfWork.SaveAsync() > 0;
            return result;
        }

        public async Task<IEnumerable<RequestHistoryDTO>> GetRequestHistoriesByAccountIdAsync(Guid accountId)
        {
            var requestHistories = await _unitOfWork.RequestHistoryRepo.GetRequestHistoriesByAccountIdAsync(accountId);
            return _mapper.Map<IEnumerable<RequestHistoryDTO>>(requestHistories);
        }

        public async Task<bool> UpdateRequestHistoryAsync(Guid requestId, RequestHistoryDTO requestHistoryDto)
        {
            var requestHistory = _mapper.Map<RequestHistory>(requestHistoryDto);
            return await _unitOfWork.RequestHistoryRepo.UpdateRequestHistoryAsync(requestId, requestHistory);
        }

        public async Task<bool> DeleteRequestHistoryAsync(Guid requestId)
        {
            return await _unitOfWork.RequestHistoryRepo.DeleteRequestHistoryAsync(requestId);
        }

        public async Task<IEnumerable<RequestHistoryDTO>> GetAllRequestHistoriesAsync()
        {
            var requestHistories = await _unitOfWork.RequestHistoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<RequestHistoryDTO>>(requestHistories);
        }
    }


}
