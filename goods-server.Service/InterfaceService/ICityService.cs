using goods_server.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetAllCitiesAsync();
        Task<CityDTO?> GetCityByIdAsync(Guid cityId);
        Task<CityDTO?> GetCityByNameAsync(string name);
        Task<bool> CreateCityAsync(CreateCityDTO city);
        Task<bool> UpdateCityAsync(Guid cityId, UpdateCityDTO city);
        Task<bool> DeleteCityAsync(Guid cityId);
    }
}
