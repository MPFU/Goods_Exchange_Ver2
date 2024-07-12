using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Core.InterfacesRepo
{
    public interface IRequestHistoryRepo : IGenericRepo<RequestHistory>
    {
        Task<IEnumerable<RequestHistory>> GetRequestHistoriesByAccountIdAsync(Guid accountId);
        Task<bool> UpdateRequestHistoryAsync(Guid requestId, RequestHistory requestHistory);
        Task<bool> DeleteRequestHistoryAsync(Guid requestId);
        Task<RequestHistory> GetRequestHistoryByIdAsync(Guid requestHistoryId);
        Task<RequestHistory?> GetByBuyerIdAsync(Guid buyerId);
        Task<RequestHistory?> GetBySellerIdAsync(Guid sellerId);
        Task<RequestHistory?> GetByProductSellerIdAsync(Guid productSellerId);
        Task<RequestHistory?> GetByProductBuyerIdAsync(Guid productBuyerId);
        Task<IEnumerable<RequestHistory>> GetAllRequestHistories();

    }
}
