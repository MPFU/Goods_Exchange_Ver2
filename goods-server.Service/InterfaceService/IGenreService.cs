using goods_server.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO?> GetGenreByIdAsync(Guid genreId);
        Task<GenreDTO?> GetGenreByNameAsync(string name);
        Task<bool> CreateGenreAsync(CreateGenreDTO genre);
        Task<bool> UpdateGenreAsync(Guid genreId, UpdateGenreDTO genre);
        Task<bool> DeleteGenreAsync(Guid genreId);
    }
}
