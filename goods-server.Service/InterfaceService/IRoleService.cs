using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IRoleService
    {
        Task<GetRoleDTO> GetRoleById(int roleId);
        Task<IEnumerable<GetRoleDTO>> GetRoles();
        Task<bool> CreateNewRole(RoleDTO roleDTO);
        Task<bool> DeleteRole(int RoleId);
        Task<bool> UpdateRole(int RoleId, RoleDTO roleDTO);
    }
}
