using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>,IRoleRepo
    {
        public RoleRepository(GoodsExchangeApplication2024DbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
        }
    }
}
