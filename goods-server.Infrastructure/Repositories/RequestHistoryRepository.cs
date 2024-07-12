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

        public async Task<RequestHistory> GetRequestHistoryByIdAsync(Guid requestHistoryId)
        {
            return await _dbContext.RequestHistories.FindAsync(requestHistoryId);
        }

        public async Task<IEnumerable<RequestHistory>> GetAllRequestHistories()
        {
            // Thay đổi dựa trên cấu trúc dữ liệu của bạn
            return await _dbContext.RequestHistories
                .Include(x => x.Buyer)
                .Include(x => x.Seller)
                .Include(x => x.ProductSeller)
                .Include(x => x.ProductBuyer)
                .ToListAsync();
        }

        public async Task<RequestHistory?> GetByBuyerIdAsync(Guid buyerId)
        {
            return await _dbContext.RequestHistories
                .Include(x => x.Buyer)
                .Where(x => x.BuyerId == buyerId)
                .FirstOrDefaultAsync();
        }

        public async Task<RequestHistory?> GetBySellerIdAsync(Guid sellerId)
        {
            return await _dbContext.RequestHistories
                .Include(x => x.Seller)
                .Where(x => x.SellerId == sellerId)
                .FirstOrDefaultAsync();
        }

        public async Task<RequestHistory?> GetByProductSellerIdAsync(Guid productSellerId)
        {
            return await _dbContext.RequestHistories
                .Include(x => x.ProductSeller)
                .Where(x => x.ProductSellerId == productSellerId)
                .FirstOrDefaultAsync();
        }

        public async Task<RequestHistory?> GetByProductBuyerIdAsync(Guid productBuyerId)
        {
            return await _dbContext.RequestHistories
                .Include(x => x.ProductBuyer)
                .Where(x => x.ProductBuyerId == productBuyerId)
                .FirstOrDefaultAsync();
        }
    }
}


