using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AuthController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginData)
        {
            try
            {
                var loginRequest = await _authService.LoginAsync(loginData);
                var acc = await _accountService.GetAccountByEmailAndPasswordAsync(loginData.Email, loginData.Password);
                if (loginRequest == null || acc == null)
                {
                    return Ok(new FailedResponseModel()
                    {
                        Status = NotFound().StatusCode,
                        Message = "Wrong email or password!"
                    });
                }
                return Ok(new SucceededResponseModel
                {
                    Status = 200,
                    Message = "Login Success",
                    Data = new
                    {
                       AccountId = acc.AccountId,
                       Username = acc.UserName,
                       Email = acc.Email,
                       AvatarImg = acc.AvatarUrl,
                       Role = acc.Role,
                       Token = loginRequest
                    }
                });
            }
            catch (NullReferenceException ex)
            {
                return NotFound(new FailedResponseModel()
                {
                    Status = NotFound().StatusCode,
                    Message = ex.Message
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerRequest)
        {
            try
            {
                var acc = await _accountService.GetAccountByEmailAsync(registerRequest.Email);
                if (acc != null)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "This Email has been used!..."
                    });
                }
                var requestResult = await _authService.RegisterAsync(registerRequest);
                if (!requestResult)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "Register failed."
                    });
                }
                return Ok(new SucceededResponseModel()
                {
                    Status = Ok().StatusCode,
                    Message = "Success",
                    Data = new
                    {
                        Account = await _accountService.GetAccountByEmailAsync(registerRequest.Email)
                    }
                });

            }
            catch (Exception ex)
            {
                return Conflict(new FailedResponseModel()
                {
                    Status = Conflict().StatusCode,
                    Message = ex.Message
                });
            }
        }
    }
}
