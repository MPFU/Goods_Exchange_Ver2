using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.InterfacesRepo;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
using goods_server.Service.FilterModel.Helper;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

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

        public async Task<GetAccount2DTO?> GetAccountByEmailAndPasswordAsync(string email, string password)
        {
            var account = await _unitOfWork.AccountRepo.GetByEmailAsync(email);
            if (account != null)
            {
                if(BCrypt.Net.BCrypt.Verify(password, account.PasswordHash))
                {
                    return _mapper.Map<GetAccount2DTO>(account);
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

        public async Task<GetAccount2DTO?> GetAccountByUsernameAsync(string username)
        {
            var account = await _unitOfWork.AccountRepo.GetByUsernameAsync(username);
            return _mapper.Map<GetAccount2DTO>(account);
        }

        public async Task<PagedResult<GetAccount2DTO>> GetAllAccountAsync<T>(AccountFilter accountFilter)
        {
            var accList = _mapper.Map<IEnumerable<GetAccount2DTO>>(await _unitOfWork.AccountRepo.GetAllAccount());
            IQueryable<GetAccount2DTO> filterAcc = accList.AsQueryable();

            // Filtering
            if (!string.IsNullOrEmpty(accountFilter.Email))
               filterAcc = filterAcc.Where(x => x.Email.Contains(accountFilter.Email, StringComparison.OrdinalIgnoreCase));
            
            if (!string.IsNullOrEmpty(accountFilter.UserName))
                filterAcc = filterAcc.Where(x => x.UserName.Contains(accountFilter.UserName, StringComparison.OrdinalIgnoreCase));
            
            if(!string.IsNullOrEmpty(accountFilter.FullName))
                filterAcc = filterAcc.Where(x => x.FullName.Contains(accountFilter.FullName, StringComparison.OrdinalIgnoreCase));

            if(!string.IsNullOrEmpty(accountFilter.Status))
                filterAcc = filterAcc.Where(x => x.Status.Contains(accountFilter.Status, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(accountFilter.RoleName))
                filterAcc = filterAcc.Where(x => x.Role.Name.Contains(accountFilter.RoleName, StringComparison.OrdinalIgnoreCase));
           
            // Sorting
            if (!string.IsNullOrEmpty(accountFilter.SortBy))
            {
                switch (accountFilter.SortBy)
                {
                    case "userName":
                        filterAcc = accountFilter.SortAscending ?
                            filterAcc.OrderBy(x => x.UserName) :
                            filterAcc.OrderByDescending(x => x.UserName);
                        break;
                    case "joinDate":
                        filterAcc = accountFilter.SortAscending ?
                            filterAcc.OrderBy(x => x.JoinDate) :
                            filterAcc.OrderByDescending(x => x.JoinDate);
                        break;
                    default:
                        filterAcc = accountFilter.SortAscending ?
                            filterAcc.OrderBy(item => GetProperty.GetPropertyValue(item, accountFilter.SortBy)) :
                            filterAcc.OrderByDescending(item => GetProperty.GetPropertyValue(item, accountFilter.SortBy)) ;
                        break;
                            
                }
            }

            // Paging
            var pageItems = filterAcc
                .Skip((accountFilter.PageNumber -1) * accountFilter.PageSize)
                .Take(accountFilter.PageSize)
                .ToList();

            return new PagedResult<GetAccount2DTO>
            {
                Items = pageItems,
                PageNumber = accountFilter.PageNumber,
                PageSize = accountFilter.PageSize,
                TotalItem = filterAcc.Count(),
                TotalPages = (int)Math.Ceiling((decimal)filterAcc.Count() / (decimal)accountFilter.PageSize)
            };
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
                    account2.AvatarUrl = (account.AvatarUrl != null) ? account.AvatarUrl : account2.AvatarUrl;
                    account2.PhoneNumber = account.PhoneNumber;
                    account2.UserName = account.UserName;
                    account2.FullName = account.FullName;               
                    account2.PasswordHash = !string.IsNullOrEmpty(account.ConfirmPassword) ? BCrypt.Net.BCrypt.HashPassword(account.ConfirmPassword) : account2.PasswordHash;
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

        public async Task<int> CheckPassword(GetAccountDTO accountDTO, string? old, string? newPass, string? confirmPass)
        {
            if(old != null)
            {
                if (BCrypt.Net.BCrypt.Verify(old, accountDTO.PasswordHash))
                {
                   var check = newPass.Equals(confirmPass) ? 1 : -1;
                    return check;
                }
            }
            return 0;
        }
      
    }
}
