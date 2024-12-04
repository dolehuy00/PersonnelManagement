using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignServ;
        private readonly TokenService _tokenServ;
        private readonly IDepartmentService _departmentServ;
        private readonly IEmployeeService _emplServ;
        /// ????
        public AssignmentController(
            IAssignmentService assignmentService, TokenService tokenService,
            IDepartmentService departmentServ, IEmployeeService emplServ)
        {
            _assignServ = assignmentService;
            _tokenServ = tokenService;
            _departmentServ = departmentServ;
            _emplServ = emplServ;
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

        [Authorize(Policy = "AllRoles")]
        [HttpPut("edit-by-leader/{departmentId}")]
        public async Task<IActionResult> EditByLeader([FromBody] AssignmentDTO assignmentDTO, long departmentId)
        {
            var titleResponse = "Update an Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentServ.IsLeaderOfDepartment(departmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, new ResponseMessageDTO(titleResponse, 403, ["Access denied!"]));
                if (assignmentDTO.ResponsiblePesonId.HasValue)
                {
                    var isPerson = await _emplServ.IsPersonOfDepartment(departmentId, assignmentDTO.ResponsiblePesonId.Value);
                    if (!isPerson) return StatusCode(400, new ResponseMessageDTO(titleResponse, 400, ["Person is not in department!"]));
                }
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

        [Authorize(Policy = "AllRoles")]
        [HttpGet("get-by-leader")]
        public async Task<IActionResult> GetByLeader(long id, long departmentId)
        {
            var titleResponse = "Get an Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentServ.IsLeaderOfDepartment(departmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, new ResponseMessageDTO(titleResponse, 403, ["Access denied!"]));
                var assignment = await _assignServ.Get(id);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
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

        [Authorize(Policy = "AllRoles")]
        [HttpGet("filter-by-leader")]
        public async Task<IActionResult> FilterByLeader([FromQuery] AssignmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentServ.IsLeaderOfDepartment(filterDTO.DepartmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, new ResponseMessageDTO(titleResponse, 403, ["Access denied!"]));
                var (results, totalPage, totalRecords) = await _assignServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpPost("add-by-leader/{departmentId}")]
        public async Task<IActionResult> AddByLeader([FromBody] AssignmentDTO assignmentDTO, long departmentId)
        {
            var titleResponse = "Create an Assignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentServ.IsLeaderOfDepartment(departmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, new ResponseMessageDTO(titleResponse, 403, ["Access denied!"]));
                if (assignmentDTO.ResponsiblePesonId.HasValue)
                {
                    var isPerson = await _emplServ.IsPersonOfDepartment(departmentId, assignmentDTO.ResponsiblePesonId.Value);
                    if (!isPerson) return StatusCode(400, new ResponseMessageDTO(titleResponse, 400, ["Person is not in department!"]));
                }
                var assignment = await _assignServ.Add(assignmentDTO);
                return Ok(new ResponseObjectDTO<AssignmentDTO>(titleResponse, [assignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpPut("change-status-by-user")]
        public async Task<IActionResult> UnLock(long id, string status)
        {
            var titleResponse = "Unlock employee.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                await _assignServ.ChangeStatusByUser(id, status, long.Parse(userIdInToken));
                return Ok(new ResponseMessageDTO(titleResponse, 200, [id.ToString(), status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
