using goods_server.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goods_server.Service.InterfaceService
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDTO loginData);
        Task<bool> RegisterAsync(RegisterDTO registerData);
    }
}
