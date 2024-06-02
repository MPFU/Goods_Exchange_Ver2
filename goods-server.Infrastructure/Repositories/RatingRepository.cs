using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepo
    {
        public RatingRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Rating?> GetByProductIdAsync(Guid productId)
        {
            return await _dbContext.Ratings.FirstOrDefaultAsync(r => r.ProductId == productId);
        }
    }
}
    