using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateAccountAsync(RegisterDTO register)
        {
            try
            {
                var account = _mapper.Map<Account>(register);
                account.JoinDate = DateTime.UtcNow;
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.PasswordHash);
                account.Status = "Active";
                account.AccountId = Guid.NewGuid();
                var check = await _unitOfWork.AccountRepo.GetByEmailAsync(account.Email);
                if(check != null)
                {
                    return false;
                }
                await _unitOfWork.AccountRepo.AddAsync(account);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAccountAsync(Guid accountId)
        {
            try
            {
                var account = await _unitOfWork.AccountRepo.GetByIdAsync(accountId);
                _unitOfWork.AccountRepo.Delete(account);
                var result = await _unitOfWork.SaveAsync() > 0;
                return result;
            }catch (DbUpdateException)
            {
                throw;
            }
            
        }

        public async Task<GetAccountDTO?> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            var account = await _unitOfWork.AccountRepo.GetByEmailAsync(email);
            if (account != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                {
                    return _mapper.Map<GetAccountDTO>(account);
                }
            }
            return null;
        }

        public async Task<AccountDTO?> GetAccountByEmailAsync(string email)
        {
            var account = await _unitOfWork.AccountRepo.GetByEmailAsync(email);
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<GetAccountDTO?> GetAccountByIdAsync(Guid accountId)
        {
            var account = await _unitOfWork.AccountRepo.GetByIdAsync(accountId);
            return _mapper.Map<GetAccountDTO>(account);
        }

        public async Task<AccountDTO?> GetAccountByUsernameAsync(string username)
        {
            var account = await _unitOfWork.AccountRepo.GetByUsernameAsync(username);
            return _mapper.Map<AccountDTO>(account);
        }

        public async Task<IEnumerable<AccountDTO>> SearchAccountsAsync(string username)
        {
            var acclist = await _unitOfWork.AccountRepo.SearchAccountByUsername(username);
            if (acclist != null)
            {
                return _mapper.Map<IEnumerable<AccountDTO>>(acclist);
            }
            return null;
        }

        public async Task<bool> UpdateAccountAsync(Guid id, UpdateProfileDTO account)
        {
            try
            {
                var account2 = await _unitOfWork.AccountRepo.GetByIdAsync(id);
                if (account2 != null)
                {
                    account2.Email = account.Email;
                    account2.AvatarUrl = account.AvatarUrl;
                    account2.PhoneNumber = account.PhoneNumber;
                    account2.UserName = account.UserName;
                    account2.FullName = account.FullName;
                    account2.Status = account.Status;
                    account2.DenyRes = account.DenyRes;
                    account2.RoleId = account.RoleId;
                    account2.PasswordHash = (account.PasswordHash != null) ? BCrypt.Net.BCrypt.HashPassword(account.PasswordHash) : account2.PasswordHash;
                    _unitOfWork.AccountRepo.Update(account2);
                    var result = await _unitOfWork.SaveAsync() > 0;
                    return result;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                throw;
            }          
        }
    }
}
