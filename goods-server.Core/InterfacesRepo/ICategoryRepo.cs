﻿using goods_server.Core.Models;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface ICategoryRepo : IGenericRepo<Category>
    {
        Task<Category?> GetByNameAsync(string name);
        Task<bool> HasProductsAsync(Guid categoryId);
    }
}
