using artshare_server.WebAPI.ResponseModels;
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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var account = await _roleService.GetRoleById(id);
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
        public async Task<IActionResult> GetAllRoleAsync()
        {
            try
            {
                var acc = await _roleService.GetRoles();
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
        [Authorize(Roles = "1")]
        public async Task<IActionResult> CreateAccount([FromBody] RoleDTO roleDTO)
        {
            try
            {
                var requestResult = await _roleService.CreateNewRole(roleDTO);
                if (!requestResult)
                {
                    return StatusCode(500, new FailedResponseModel
                    {
                        Status = 500,
                        Message = "Create Role failed."
                    });
                }
                return Ok();

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
        public async Task<IActionResult> UpdateRole(int id, RoleDTO roleDTO)
        {
            try
            {
                var check = await _roleService.GetRoleById(id);
                if (check == null)
                {
                    return NotFound();
                }
                var up = await _roleService.UpdateRole(id, roleDTO);
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
        [Authorize(Roles = "1")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var check = await _roleService.GetRoleById(id);
                if (check == null)
                {
                    return NotFound();
                }
                var del = await _roleService.DeleteRole(id);
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
