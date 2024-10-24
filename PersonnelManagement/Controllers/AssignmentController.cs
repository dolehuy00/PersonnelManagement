using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignServ;
        private readonly TokenService _tokenServ;

        public AssignmentController(IAssignmentService assignmentService, TokenService tokenService)
        {
            _assignServ = assignmentService;
            _tokenServ = tokenService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AssignmentDTO assignmentDTO)
        {
            var titleResponse = "Create an Assignment.";
            try
            {
                var assignment = await _assignServ.Add(assignmentDTO);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] AssignmentDTO assignmentDTO)
        {
            var titleResponse = "Update an Assignment.";
            try
            {
                var assignment = await _assignServ.Edit(assignmentDTO);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
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
            var titleResponse = "Delete an Assignment.";
            try
            {
                await _assignServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete Assignment id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMany([FromQuery] long[] id)
        {
            var titleResponse = "Delete many Assignment.";
            try
            {
                var messages = await _assignServ.DeleteMany(id);
                return Ok(new ResponseMessageDTO(titleResponse, messages));
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
            var titleResponse = "Get an Assignment.";
            try
            {
                var assignment = await _assignServ.Get(id);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("get/{page}/{itemPerPage}")]
        public async Task<IActionResult> Get(int page, int itemPerPage)
        {
            var titleResponse = "Get page Assignment.";
            try
            {
                var (assignments, totalPage, totalRecords) = await _assignServ.GetPagesAsync(page, itemPerPage);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, assignments, page, totalPage, totalRecords));
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
            var titleResponse = "Get all Assignment.";
            try
            {
                var assignments = await _assignServ.GetAll();
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, assignments, 1, 1, assignments.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] AssignmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter Assignment.";
            try
            {
                var (results, totalPage, totalRecords) = await _assignServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("get-by-user/{id}")]
        public async Task<IActionResult> GetByUser(long id)
        {
            var titleResponse = "Get an Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var assignment = await _assignServ.GetByEmployee(id, long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("get-by-user/{page}/{itemPerPage}")]
        public async Task<IActionResult> GetByUser(int page, int itemPerPage)
        {
            var titleResponse = "Get page Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var (assignments, totalPage, totalRecords) = await _assignServ.GetPagesByEmployeeAsync(page, itemPerPage, long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, assignments, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("filter-by-user")]
        public async Task<IActionResult> FilterByUser([FromQuery] AssignmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var (results, totalPage, totalRecords) = await _assignServ.FilterByUserAsync(filterDTO, long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
