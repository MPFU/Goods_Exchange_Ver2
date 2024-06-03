using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class RequestHistoryRepository : GenericRepository<RequestHistory>, IRequestHistoryRepo
    {
        public RequestHistoryRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<RequestHistory>> GetRequestHistoriesByAccountIdAsync(Guid accountId)
        {
            return await _dbContext.RequestHistories.Where(x => x.BuyerId == accountId || x.SellerId == accountId).ToListAsync();
        }

        public async Task<bool> UpdateRequestHistoryAsync(Guid requestId, RequestHistory requestHistory)
        {
            var existingRequestHistory = await _dbContext.RequestHistories.FindAsync(requestId);
            if (existingRequestHistory == null)
            {
                return false;
            }

            existingRequestHistory.Status = requestHistory.Status;
            _dbContext.RequestHistories.Update(existingRequestHistory);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRequestHistoryAsync(Guid requestId)
        {
            var requestHistory = await _dbContext.RequestHistories.FindAsync(requestId);
            if (requestHistory == null)
            {
                return false;
            }

            _dbContext.RequestHistories.Remove(requestHistory);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}
