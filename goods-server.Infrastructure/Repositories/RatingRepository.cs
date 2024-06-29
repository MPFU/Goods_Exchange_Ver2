using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepo
    {
        public RatingRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Rating?> GetByCustomerAndProductIdAsync(Guid customerId, Guid productId)
        {
            return await _dbContext.Ratings.FirstOrDefaultAsync(r => r.CustomerId == customerId && r.ProductId == productId);
        }

        public async Task<IEnumerable<Rating>> FindAsync(Func<Rating, bool> predicate) // Implement this method
        {
            return await Task.FromResult(_dbContext.Ratings.Where(predicate));
        }
    }
}
