using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
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
    }
}
