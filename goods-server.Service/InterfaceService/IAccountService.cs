using goods_server.Contracts;
using goods_server.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IAccountService
    {
        Task<AccountDTO?> GetAccountByEmailAsync(string email);

        Task<AccountDTO?> GetAccountByUsernameAsync(string username);

        Task<GetAccountDTO?> GetAccountByEmailAndPasswordAsync(string email, string password);

        Task<bool> CreateAccountAsync(RegisterDTO account);

        Task<IEnumerable<AccountDTO>> SearchAccountsAsync(string username);

        Task<bool> UpdateAccountAsync(Guid id, UpdateProfileDTO account);

        Task<GetAccountDTO?> GetAccountByIdAsync(Guid accountId);

        Task<bool> DeleteAccountAsync(Guid accountId);
    }
}
