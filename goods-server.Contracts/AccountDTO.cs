using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterDTO
    {
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string UserName { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public int RoleId { get; set; }      
    }

    public class UpdateProfileDTO : RegisterDTO
    {
        public string? AvatarUrl { get; set; }

        public string? DenyRes { get; set; }

        public string? Status { get; set; }
    }

    public class AccountDTO
    {
        public string? Email { get; set; }

        public string? PasswordHash { get; set; }

        public string? AvatarUrl { get; set; }

        public string? UserName { get; set; }

        public string? FullName { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? JoinDate { get; set; }

        public int? RoleId { get; set; }

        public string? DenyRes { get; set; }

        public string? Status { get; set; }

    }

    public class GetAccountDTO : AccountDTO
    {
        public Guid AccountId { get; set; }
    }
}
