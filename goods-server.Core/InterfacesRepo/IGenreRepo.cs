using goods_server.Core.Models;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IGenreRepo : IGenericRepo<Genre>
    {
        Task<Genre?> GetByNameAsync(string name);
        Task<bool> HasProductsAsync(Guid genreId);
    }
}
