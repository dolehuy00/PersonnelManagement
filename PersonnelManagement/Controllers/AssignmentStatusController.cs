using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    public class AssignmentStatusController : Controller
    {
        private readonly IAssignmentStatusService _statusServ;

        public AssignmentStatusController(IAssignmentStatusService statusService)
        {
            _statusServ = statusService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AssignmentStatusDTO statusDTO)
        {
            var titleResponse = "Create a assignment status.";
            try
            {
                var status = await _statusServ.Add(statusDTO);
                return Ok(new ResponseObjectDTO<AssignmentStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] AssignmentStatusDTO statusDTO)
        {
            var titleResponse = "Update a assignment status.";
            try
            {
                var account = await _statusServ.Edit(statusDTO);
                return Ok(new ResponseObjectDTO<AssignmentStatusDTO>(titleResponse, [account]));
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
            var titleResponse = "Delete a assignment status.";
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
            var titleResponse = "Get a assignment status.";
            try
            {
                var status = await _statusServ.Get(id);
                return Ok(new ResponseObjectDTO<AssignmentStatusDTO>(titleResponse, [status]));
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
            var titleResponse = "Get all assignment status.";
            try
            {
                var statuses = await _statusServ.GetAll();
                return Ok(new ResponseObjectDTO<AssignmentStatusDTO>(titleResponse, statuses, 1, 1, statuses.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
