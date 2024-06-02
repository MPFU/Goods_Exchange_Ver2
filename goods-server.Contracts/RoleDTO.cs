using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Contracts
{
    public class RoleDTO
    {
        public string? Name { get; set; }
    }

    public class GetRoleDTO : RoleDTO
    {
        public int RoleId { get; set; }
    }
}
