using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Core.Interfaces
{
    public interface IAccountRepo : IGenericRepo<Account>
    {
        Task<Account?> GetByEmailAsync(string email);
        Task<IEnumerable<Account>> SearchAccountByUsername(string username);
        Task<Account?> GetByUsernameAsync(string username);
    }
}
