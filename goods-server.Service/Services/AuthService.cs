using AutoMapper;
using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.InterfaceService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IAccountService accountService, IMapper mapper, IConfiguration configuration)
        {
            _accountService = accountService;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginDTO loginData)
        {
            var account = await _accountService.GetAccountByEmailAndPasswordAsync(loginData.Email, loginData.Password);
            if (account == null)
            {
                return null;
            }
            return CreateToken(account);
        }

        public async Task<bool> RegisterAsync(RegisterDTO registerData)
        {
            try
            {                                       
                var result = await _accountService.CreateAccountAsync(registerData);
                return result;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CreateToken(GetAccount2DTO account)
        {
            var nowUtc = DateTime.UtcNow;
            var expirationDuration = TimeSpan.FromMinutes(60);
            var expirationUtc = nowUtc.Add(expirationDuration);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,
                        _configuration.GetSection("JwtSecurityToken:Subject").Value),
                new Claim(JwtRegisteredClaimNames.Jti,
                                  Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                                  EpochTime.GetIntDate(nowUtc).ToString(),
                                  ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Exp,
                                  EpochTime.GetIntDate(expirationUtc).ToString(),
                                  ClaimValueTypes.Integer64),
                new Claim(JwtRegisteredClaimNames.Iss,
                                  _configuration.GetSection("JwtSecurityToken:Issuer").Value),
                new Claim(JwtRegisteredClaimNames.Aud,
                                  _configuration.GetSection("JwtSecurityToken:Audience").Value),
                new Claim(ClaimTypes.Email, account.Email),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim(ClaimTypes.UserData, account.UserName.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (_configuration.GetSection("JwtSecurityToken:Key").Value));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                        issuer: _configuration.GetSection("JwtSecurityToken:Issuer").Value,
                        audience: _configuration.GetSection("JwtSecurityToken:Audience").Value,
                        claims: claims,
                        expires: expirationUtc,
                        signingCredentials: signIn);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
