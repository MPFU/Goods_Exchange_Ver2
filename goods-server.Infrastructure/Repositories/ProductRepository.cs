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
    public class ProductRepository : GenericRepository<Product>,IProductRepo
    {
        public ProductRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _dbContext.Products
                .Include(x => x.City)
                .Include(x => x.Category)
                .Include(x => x.Genre)
                .ToListAsync();
        }
    }
}
