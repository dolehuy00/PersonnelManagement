using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Enum;
using PersonnelManagement.Services;
using System.Diagnostics;

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private IDepartmentService _deptService;

        public DepartmentController(IDepartmentService deptService)
        {
            _deptService = deptService ?? throw new ArgumentNullException(nameof(deptService));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] DepartmentDTO departmentDTO)
        {
            var titleResponse = "Create an department.";
            try
            {
                var department = await _deptService.Add(departmentDTO);
                return Ok(new ResponseObjectDTO<DepartmentDTO>(titleResponse, [department]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] DepartmentDTO departmentDTO)
        {
            var titleResponse = "Update an department.";
            try
            {
                var department = await _deptService.Edit(departmentDTO);
                return Ok(new ResponseObjectDTO<DepartmentDTO>(titleResponse, [department]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var titleResponse = "Delete an department.";
            try
            {
                await _deptService.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete department id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMany([FromQuery] long[] id)
        {
            var titleResponse = "Delete many department.";
            try
            {
                var messages = await _deptService.DeleteMany(id);
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
            var titleResponse = "Get an department.";
            try
            {
                var department = await _deptService.Get(id);
                return Ok(new ResponseObjectDTO<DepartmentDTO>(titleResponse, [department]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }


        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] DepartmentFilterDTO filterDTO)
        {
            var titleResponse = "Filter department.";
            try
            {
                var (results, totalPage, totalRecords) = await _deptService.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<DepartmentDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Chi tiết: {ex.StackTrace}");
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("changeStatus/{id}")]
        public async Task<IActionResult> changeStatus(long id)
        {
            var titleResponse = "Get an department.";
            try
            {
                String status = await _deptService.changeStatus(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [id.ToString(), status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
