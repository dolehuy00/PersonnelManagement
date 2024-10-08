using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.Service;
using PersonnelManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private IEmployeeService _emplServ;
        private readonly JwtTokenService _jwtTokenServ;
        private JsonResponseService _jsonResponseServ;

        public EmployeeController(IEmployeeService employeeService)
        {
            _emplServ = employeeService;
            _jwtTokenServ = new JwtTokenService();
            _jsonResponseServ = new JsonResponseService();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = await _emplServ.Add(employeeDTO);
                return Ok(_jsonResponseServ.OkOneEmployeeResponse("Create an employee.", [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Create an employee.", [ex.Message]));
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                var employee = await _emplServ.Edit(employeeDTO);
                return Ok(_jsonResponseServ.OkOneEmployeeResponse("Update an employee.", [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Update an employee.", [ex.Message]));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _emplServ.Delete(id);
                return Ok(_jsonResponseServ.OkMessageResponse("Delete an employee.", [$"Delete employee id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Delete an employee.", [ex.Message]));
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMany([FromQuery] long[] id)
        {
            try
            {
                var messages = await _emplServ.DeleteMany(id);
                return Ok(_jsonResponseServ.OkMessageResponse("Delete many employee.", messages));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Delete many employee.", [ex.Message]));
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var employee = await _emplServ.Get(id);
                return Ok(_jsonResponseServ.OkOneEmployeeResponse("Get an employee.", [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get an employee.", [ex.Message]));
            }
        }

        [HttpGet("get/{page}/{itemPerPage}")]
        public async Task<IActionResult> Get(int page, int itemPerPage)
        {
            try
            {
                var (employees, totalPage, totalRecords) = await _emplServ.GetPagesAsync(page, itemPerPage);
                return Ok(_jsonResponseServ.OkListEmployeeResponse("Get page employee.", employees, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get page employee.", [ex.Message]));
            }
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _emplServ.GetAll();
                return Ok(_jsonResponseServ.OkListEmployeeResponse("Get all employee.", employees, 1, 1, employees.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get all employee.", [ex.Message]));
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] EmployeeFilterDTO filterDTO)
        {
            try
            {
                var (results, totalPage, totalRecords) = await _emplServ.FilterAsync(filterDTO);
                return Ok(_jsonResponseServ.OkListEmployeeResponse("Filter employee.", results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Filter employee.", [ex.Message]));
            }
        }
    }
}
