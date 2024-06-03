using goods_server.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId);
        Task<CategoryDTO?> GetCategoryByNameAsync(string name);
        Task<bool> CreateCategoryAsync(CreateCategoryDTO category);
        Task<bool> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category);
        Task<bool> DeleteCategoryAsync(Guid categoryId);
    }
}
