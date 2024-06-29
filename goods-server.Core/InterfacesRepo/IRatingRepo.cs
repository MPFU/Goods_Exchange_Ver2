using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IRatingRepo : IGenericRepo<Rating>
    {
        Task<Rating?> GetByCustomerAndProductIdAsync(Guid customerId, Guid productId);
        Task<IEnumerable<Rating>> FindAsync(Func<Rating, bool> predicate); // Add this line
    }
}
