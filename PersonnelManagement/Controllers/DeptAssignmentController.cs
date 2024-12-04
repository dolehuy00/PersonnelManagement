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
    public class DeptAssignmentController : ControllerBase
    {
        private IDeptAssignmentService _deptAssignmentService;
        private readonly TokenService _tokenServ;
        private IDepartmentService _departmentService;

        public DeptAssignmentController(IDeptAssignmentService deptAssignmentService, TokenService tokenServ, IDepartmentService departmentService)
        {
            _deptAssignmentService = deptAssignmentService ?? throw new ArgumentNullException(nameof(deptAssignmentService));
            _tokenServ = tokenServ;
            _departmentService = departmentService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DeptAssignmentDTO deptAssignmentDTO)
        {
            var titleResponse = "Create an deptAssignment.";
            try
            {
                var deptAssignment = await _deptAssignmentService.Add(deptAssignmentDTO);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, [deptAssignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] DeptAssignmentDTO deptAssignmentDTO)
        {
            var titleResponse = "Update an deptAssignment.";
            try
            {
                var deptAssignment = await _deptAssignmentService.Edit(deptAssignmentDTO);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, [deptAssignment]));
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
            var titleResponse = "Delete an deptAssignment.";
            try
            {
                await _deptAssignmentService.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete deptAssignment id = {id} successfully."]));
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
            var titleResponse = "Delete many deptAssignment.";
            try
            {
                var messages = await _deptAssignmentService.DeleteMany(id);
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
            var titleResponse = "Get an deptAssignment.";
            try
            {
                var deptAssignment = await _deptAssignmentService.Get(id);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, [deptAssignment]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] DeptAssignmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter deptAssignment.";
            try
            {
                var (results, totalPage, totalRecords) = await _deptAssignmentService.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddMany")]
        public async Task<IActionResult> AddMany([FromBody] List<DeptAssignmentDTO> deptAssignmentDTOs)
        {
            //
            var titleResponse = "Create multiple deptAssignments.";
            try
            {
                var deptAssignments = await _deptAssignmentService.AddMany(deptAssignmentDTOs);
                // Trả về danh sách kết quả đã thêm
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, deptAssignments));
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("EditMany/{projectId}")]
        public async Task<IActionResult> AddMany(long projectId, [FromBody] List<DeptAssignmentDTO> deptAssignmentDTOs)
        {
            var titleResponse = "Edit multiple deptAssignments.";
            try
            {
                var deptAssignments = await _deptAssignmentService.EditManyByProjectId(projectId, deptAssignmentDTOs);
                // Trả về danh sách kết quả đã thêm
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, deptAssignments));
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchId(long id)
        {
            var titleResponse = "Search dept assignment";
            try
            {
                var results = await _deptAssignmentService.SearchIdAsync(id);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, results, 1, 1, results.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("filter-by-leader")]
        public async Task<IActionResult> FilterByLeader([FromQuery] DeptAssignmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter deptAssignment.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentService.IsLeaderOfDepartment(filterDTO.departmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, "Access denied! ");
                var (results, totalPage, totalRecords) = await _deptAssignmentService.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<DeptAssignmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
