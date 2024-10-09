using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    public class EmployeeStatusController : Controller
    {
        private readonly IEmployeeStatusService _statusServ;

        public EmployeeStatusController(IEmployeeStatusService statusService)
        {
            _statusServ = statusService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] EmployeeStatusDTO statusDTO)
        {
            var titleResponse = "Create a employee status.";
            try
            {
                var status = await _statusServ.Add(statusDTO);
                return Ok(new ResponseObjectDTO<EmployeeStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EmployeeStatusDTO statusDTO)
        {
            var titleResponse = "Update a employee status.";
            try
            {
                var account = await _statusServ.Edit(statusDTO);
                return Ok(new ResponseObjectDTO<EmployeeStatusDTO>(titleResponse, [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var titleResponse = "Delete a employee status.";
            try
            {
                await _statusServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete account id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var titleResponse = "Get a employee status.";
            try
            {
                var status = await _statusServ.Get(id);
                return Ok(new ResponseObjectDTO<EmployeeStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var titleResponse = "Get all employee status.";
            try
            {
                var statuses = await _statusServ.GetAll();
                return Ok(new ResponseObjectDTO<EmployeeStatusDTO>(titleResponse, statuses, 1, 1, statuses.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
