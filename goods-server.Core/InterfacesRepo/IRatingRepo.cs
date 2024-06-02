using goods_server.Core.Models;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IRatingRepo : IGenericRepo<Rating>
    {
        Task<Rating?> GetByProductIdAsync(Guid productId);
    }
}
