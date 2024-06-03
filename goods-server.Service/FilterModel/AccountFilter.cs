using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.FilterModel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.FilterModel
{
    public class AccountFilter : FilterOption<Account>
    {
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Status { get; set; }
        public string? RoleName { get; set; }
    }
}
