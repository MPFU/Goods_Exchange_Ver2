using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepo
    {
        public GenreRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Genre?> GetByNameAsync(string name)
        {
            return await _dbContext.Genres.FirstOrDefaultAsync(g => g.Name == name);
        }

        public async Task<bool> HasProductsAsync(Guid genreId)
        {
            return await _dbContext.Products.AnyAsync(p => p.GenreId == genreId);
        }
    }
}
