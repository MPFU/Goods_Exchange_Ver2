using goods_server.Core.Models;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IRatingRepo : IGenericRepo<Rating>
    {
        Task<Rating?> GetByCustomerAndProductIdAsync(Guid customerId, Guid productId);
        Task<int?> CountRatingbyProId(Guid? proId);
    }
}
