using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
using goods_server.Service.FilterModel.Helper;
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

        Task<GetAccount2DTO?> GetAccountByUsernameAsync(string username);

        Task<GetAccount2DTO?> GetAccountByEmailAndPasswordAsync(string email, string password);

        Task<bool> CreateAccountAsync(RegisterDTO account);

        Task<IEnumerable<AccountDTO>> SearchAccountsAsync(string username);

        Task<bool> UpdateAccountAsync(Guid id, UpdateProfileDTO account);

        Task<GetAccountDTO?> GetAccountByIdAsync(Guid accountId);

        Task<bool> DeleteAccountAsync(Guid accountId);

        Task<PagedResult<GetAccount2DTO>> GetAllAccountAsync<T>(AccountFilter accountFilter);

        Task<int> CheckPassword(GetAccountDTO accountDTO, string? old, string? newPass, string? confirmPass);
    }
}
