using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepo
    {
        public RatingRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int?> CountRatingbyProId(Guid? proId)
        {
            return await _dbContext.Ratings.Where(x => x.ProductId.Equals(proId)).CountAsync();
        }

        public async Task<Rating?> GetByCustomerAndProductIdAsync(Guid customerId, Guid productId)
        {
            return await _dbContext.Ratings.FirstOrDefaultAsync(r => r.CustomerId == customerId && r.ProductId == productId);
        }
    }
}
