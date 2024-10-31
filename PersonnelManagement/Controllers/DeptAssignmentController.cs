using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeptAssignmentController : ControllerBase
    {
        private IDeptAssignmentService _deptAssignmentService;

        public DeptAssignmentController(IDeptAssignmentService deptAssignmentService)
        {
            _deptAssignmentService = deptAssignmentService ?? throw new ArgumentNullException(nameof(deptAssignmentService));
        }

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
    }
}
