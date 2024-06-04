﻿using artshare_server.WebAPI.ResponseModels;
using goods_server.Contracts;
using goods_server.Core.Models;
using goods_server.Service.FilterModel;
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
        private readonly IAzureBlobStorage _azureBlobStorage;

        public AccountController(IAccountService accountService, IAzureBlobStorage azureBlobStorage)
        {
            _accountService = accountService;
            _azureBlobStorage = azureBlobStorage;
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

        [HttpGet]
        public async Task<IActionResult> GetAllAccountAsync([FromQuery] AccountFilter accountFilter)
        {
            try
            {
                var acc = await _accountService.GetAllAccountAsync<Account>(accountFilter);
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
                var up = await _accountService.UpdateAccountAsync(id, profileDTO);
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

        [HttpPost]
        public async Task<IActionResult> UploadFileAvatar(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest(new FailedResponseModel()
                    {
                        Status = BadRequest().StatusCode,
                        Message = "File is not selected or empty."
                    });
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png" };
                if(imageExtensions.Any(e => file.FileName.EndsWith(e, StringComparison.OrdinalIgnoreCase)) == false)
                {
                    return BadRequest(new FailedResponseModel()
                    {
                        Status = BadRequest().StatusCode,
                        Message = "File is not image."
                    });
                }
                var containerName = "avatar"; // replace with your container name
                var uri = await _azureBlobStorage.UploadFileAsync(containerName, file);

                return Ok(new SucceededResponseModel()
                {
                    Status = Ok().StatusCode,
                    Message = "File uploaded successfully",
                    Data = new
                    {
                        FileUri = uri
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new FailedResponseModel
                {
                    Status = 500,
                    Message = $"An error occurred while uploading the file: {ex.Message}"
                });
            }
        }
    }
}
