using goods_server.Contracts;
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
        Task<IEnumerable<RequestHistoryDTO>> GetRequestHistoriesByAccountIdAsync(Guid accountId);
        Task<IEnumerable<RequestHistoryDTO>> GetAllRequestHistoriesAsync();
        Task<bool> UpdateRequestHistoryAsync(Guid requestId, RequestHistoryDTO requestHistory);
        Task<bool> DeleteRequestHistoryAsync(Guid requestId);

    }
}
