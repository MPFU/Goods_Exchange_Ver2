using goods_server.Core.Models;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface ICityRepo : IGenericRepo<City>
    {
        Task<City?> GetByNameAsync(string name);
    }
}
