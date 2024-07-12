using goods_server.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepo<T> where T : class
    {
        protected readonly GoodsExchangeApplication2024DbContext _dbContext;

        public GenericRepository(GoodsExchangeApplication2024DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T?> GetByIdAsync(Guid? id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}