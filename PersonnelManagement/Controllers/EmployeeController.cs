using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.Service;
using PersonnelManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Controllers
{
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

        [Authorize(Roles = "Admin")]
        [HttpGet("add")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("edit")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteMany([FromBody] long[] ids)
        {
            try
            {
                var messages = await _emplServ.DeleteMany(ids);
                return Ok(_jsonResponseServ.OkMessageResponse("Delete many employee.", messages));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Delete many employee.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{page}/{itemPerPage}")]
        public async Task<IActionResult> Get(int page, int itemPerPage)
        {
            try
            {
                var (employees, totalPage, totalRecords) = await _emplServ.GetPagedListWithTotalPagesAsync(page, itemPerPage);
                return Ok(_jsonResponseServ.OkListEmployeeResponse("Get page employee.", employees, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get page employee.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string keyword)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Filter employee.", [ex.Message]));
            }
        }
    }
}
