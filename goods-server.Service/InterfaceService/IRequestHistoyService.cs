﻿using goods_server.Contracts;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.FilterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IRequestHistoyService
    {
        Task<bool> CreateRequestHistoryAsync(RequestHistoryDTO requestHistory);
        Task<PagedResult<GetRequestHistoryDTO>> GetAllRequestHistoriesAsync(RequestHistoryFilter filter);
        Task<PagedResult<GetRequestHistoryDTO>> GetRequestHistoriesByAccountIdAsync(RequestHistoryFilter filter);
        Task<bool> UpdateRequestHistoryAsync(Guid requestId, UpdateRequestHistoryDTO requestHistory);
        Task<bool> DeleteRequestHistoryAsync(Guid requestId);
        Task<GetRequestHistoryDTO> GetRequestHistoryByIdAsync(Guid requestHistoryId);


    }
}
