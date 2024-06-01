using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Service.InterfaceService;
using goods_server.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace goods_server.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            try
            {
                var account = await _accountService.GetAccountByIdAsync(id);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetAccountByEmail(string email)
        {
            try
            {
                var account = await _accountService.GetAccountByEmailAsync(email);
                if (account == null)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, UpdateProfileDTO profileDTO)
        {
            try
            {
                var check = await _accountService.GetAccountByIdAsync(id);
                if (check == null)
                {
                    return NotFound();
                }
                var up = await _accountService.UpdateAccountAsync(id,profileDTO);
                if (up)
                {
                    return Ok("Update SUCCESS!");
                }
                return BadRequest("Update FAIL");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{username}")]
        public async Task<IActionResult> SearchByUsername(string username)
        {
            try
            {
                var acc = await _accountService.GetAccountByUsernameAsync(username);
                if (acc == null)
                {
                    return NotFound();
                }
                return Ok(acc);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> SearchByUsernameToList(string username)
        {
            try
            {
                var acc = await _accountService.SearchAccountsAsync(username);
                if (acc == null)
                {
                    return NotFound();
                }
                return Ok(acc);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize (Roles = "1")]
        public async Task<IActionResult> CreateAccount([FromBody] RegisterDTO registerRequest)
        {
            try
            {
                var requestResult = await _accountService.CreateAccountAsync(registerRequest);
                if (!requestResult)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "Create account failed."
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
       
        [HttpDelete("{id}")]
        [Authorize (Roles = "1")]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            try
            {
                var check = await _accountService.GetAccountByIdAsync(id);
                if (check == null)
                {
                    return NotFound();
                }
                var del = await _accountService.DeleteAccountAsync(id);
                if (del)
                {
                    return Ok("Delete SUCCESS!");
                }
                return BadRequest("Delete FAIL");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
