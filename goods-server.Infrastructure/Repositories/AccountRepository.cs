using goods_server.Core.Interfaces;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class AccountRepository : GenericRepository<Account>,IAccountRepo
    {
        public AccountRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext) 
        { 

        }

        public async Task<IEnumerable<Account>> GetAllAccount()
        {
            return await _dbContext.Accounts.Include(x => x.Role).ToListAsync();
        }

        public async Task<Account?> GetByEmailAsync(string email)
        {
            return await _dbContext.Accounts
                .Include(x => x.Role)
                .Where(x => x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Account?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Accounts
                .Include(x => x.Role)
                .Where(x => x.UserName == username)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Account>> SearchAccountByUsername(string username)
        {
            return await _dbContext.Accounts.Where(x => x.UserName.Contains(username)).ToListAsync();
        }
    }
}
