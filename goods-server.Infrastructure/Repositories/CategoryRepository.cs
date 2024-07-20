using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepo
    {
        public CategoryRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category?> GetByNameAsync(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<bool> HasProductsAsync(Guid categoryId)
        {
            return await _dbContext.Products.AnyAsync(p => p.CategoryId == categoryId);
        }
    }
}
