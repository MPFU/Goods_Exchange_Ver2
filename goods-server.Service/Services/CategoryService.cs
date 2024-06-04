using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO?> GetCategoryByNameAsync(string name)
        {
            var category = await _unitOfWork.CategoryRepo.GetByNameAsync(name);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> CreateCategoryAsync(CreateCategoryDTO category)
        {
            var newCategory = _mapper.Map<Category>(category);
            newCategory.CategoryId = Guid.NewGuid();
            await _unitOfWork.CategoryRepo.AddAsync(newCategory);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> UpdateCategoryAsync(Guid categoryId, UpdateCategoryDTO category)
        {
            var existingCategory = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId);
            if (existingCategory == null)
            {
                return false;
            }

            _mapper.Map(category, existingCategory);
            _unitOfWork.CategoryRepo.Update(existingCategory);
            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            _unitOfWork.CategoryRepo.Delete(category);
            return await _unitOfWork.SaveAsync() > 0;
        }
    }
}
