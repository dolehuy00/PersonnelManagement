using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class ProjectStatusController : Controller
    {
        private readonly IProjectStatusService _statusServ;

        public ProjectStatusController(IProjectStatusService statusService)
        {
            _statusServ = statusService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProjectStatusDTO statusDTO)
        {
            var titleResponse = "Create a project status.";
            try
            {
                var status = await _statusServ.Add(statusDTO);
                return Ok(new ResponseObjectDTO<ProjectStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] ProjectStatusDTO statusDTO)
        {
            var titleResponse = "Update a project status.";
            try
            {
                var account = await _statusServ.Edit(statusDTO);
                return Ok(new ResponseObjectDTO<ProjectStatusDTO>(titleResponse, [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var titleResponse = "Delete a project status.";
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

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var titleResponse = "Get a project status.";
            try
            {
                var status = await _statusServ.Get(id);
                return Ok(new ResponseObjectDTO<ProjectStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var titleResponse = "Get all project status.";
            try
            {
                var statuses = await _statusServ.GetAll();
                return Ok(new ResponseObjectDTO<ProjectStatusDTO>(titleResponse, statuses, 1, 1, statuses.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
