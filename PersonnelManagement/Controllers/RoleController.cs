using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleServ;

        public RoleController(IRoleService roleService)
        {
            _roleServ = roleService;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var titleResponse = "Get all Role.";
            try
            {
                var roles = await _roleServ.GetAll();
                return Ok(new ResponseObjectDTO<RoleDTO>(titleResponse, roles, 1, 1, roles.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
