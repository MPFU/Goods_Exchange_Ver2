using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepo
    {
        public OrderDetailRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<OrderDetail?> GetByOrderAndProductIdAsync(Guid orderId, Guid productId)
        {
            return await _dbContext.OrderDetails.FirstOrDefaultAsync(od => od.OrderId == orderId && od.ProductId == productId);
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid OrderID)
        {
            return await _dbContext.OrderDetails.Include(x=> x.Product).Where(x=> x.OrderId.Equals(OrderID)).ToListAsync();
        }
    }
}
