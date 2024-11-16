using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.Enum;
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
        private readonly TokenService _tokenServ;

        public EmployeeController(IEmployeeService employeeService, TokenService tokenService)
        {
            _emplServ = employeeService;
            _tokenServ = tokenService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] EmployeeDTO employeeDTO)
        {
            var titleResponse = "Create an employee.";
            try
            {
                var employee = await _emplServ.Add(employeeDTO);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EmployeeDTO employeeDTO)
        {
            var titleResponse = "Update an employee.";
            try
            {
                var employee = await _emplServ.Edit(employeeDTO);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var titleResponse = "Delete an employee.";
            try
            {
                await _emplServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete employee id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMany([FromQuery] long[] id)
        {
            var titleResponse = "Delete many employee.";
            try
            {
                var messages = await _emplServ.DeleteMany(id);
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
            var titleResponse = "Get an employee.";
            try
            {
                var employee = await _emplServ.Get(id);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpGet("get/{page}/{itemPerPage}")]
        public async Task<IActionResult> Get(int page, int itemPerPage)
        {
            var titleResponse = "Get page employee.";
            try
            {
                var (employees, totalPage, totalRecords) = await _emplServ.GetPagesAsync(page, itemPerPage);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, employees, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var titleResponse = "Get all employee.";
            try
            {
                var employees = await _emplServ.GetAll();
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, employees, 1, 1, employees.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] EmployeeFilterDTO filterDTO)
        {
            var titleResponse = "Filter employee.";
            try
            {
                var (results, totalPage, totalRecords) = await _emplServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("lock/{id}")]
        public async Task<IActionResult> Lock(long id)
        {
            var titleResponse = $"Lock employee id = {id}.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock yourself out!");
                var results = await _emplServ.Lock(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [Status.Lock]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [HttpPut("unlock/{id}")]
        public async Task<IActionResult> UnLock(long id)
        {
            var titleResponse = "Unlock employee.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock yourself out!");
                var results = await _emplServ.UnLock(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [Status.Active]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
