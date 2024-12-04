using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Enum;
using PersonnelManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeService _emplServ;
        private readonly TokenService _tokenServ;
        private readonly IStaticFileService _staticFileServ;
        private readonly IDepartmentService _departmentServ;

        public EmployeeController(IEmployeeService employeeService, TokenService tokenService, IStaticFileService staticFileServ, IDepartmentService departmentServ)
        {
            _emplServ = employeeService;
            _tokenServ = tokenService;
            _staticFileServ = staticFileServ;
            _departmentServ = departmentServ;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm] EmployeeDTO employeeDTO)
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

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AllRoles")]
        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var titleResponse = "Get an employee.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var employee = await _emplServ.Get(long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, [employee]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string fullnameOrId, long? departmentId)
        {
            var titleResponse = "Get page employee.";
            try
            {
                var employees = await _emplServ.SearchNameOrIdAsync(fullnameOrId, departmentId);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, employees, 1, 1, employees.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("search-by-leader")]
        public async Task<IActionResult> SearchByLeader(string fullnameOrId, long? departmentId)
        {
            var titleResponse = "Get page employee.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isLeader = await _departmentServ.IsLeaderOfDepartment(departmentId, long.Parse(userIdInToken));
                if (!isLeader) return StatusCode(403, "Access denied!");
                var employees = await _emplServ.SearchNameOrIdAsync(fullnameOrId, departmentId);
                return Ok(new ResponseObjectDTO<EmployeeDTO>(titleResponse, employees, 1, 1, employees.Count));
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

        [Authorize(Policy = "AdminOnly")]
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

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("lock/{id}")]
        public async Task<IActionResult> Lock(long id)
        {
            var titleResponse = $"Lock employee id = {id}.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock yourself out!");
                var results = await _emplServ.Lock(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [id.ToString(), Status.Lock]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("unlock/{id}")]
        public async Task<IActionResult> UnLock(long id)
        {
            var titleResponse = "Unlock employee.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock yourself out!");
                var results = await _emplServ.UnLock(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [id.ToString(), Status.Active]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("change-image/{id}")]
        public async Task<IActionResult> ChangeImageByAdmin(IFormFile file, long id)
        {
            var titleResponse = "Change image employee.";
            try
            {
                //Generate token for authen server storage file
                var key = _tokenServ.GenerateAccessTokenImgServer();
                // Create file name
                var fileName = $"avatar-user-{id}.{file.FileName.Split(".").Last()}";
                // Gọi service để upload file
                var fileUrl = await _staticFileServ.UploadImageAsync(file, fileName, key);
                // Update url image in database
                await _emplServ.UpdateImageAsync(id, fileUrl);

                return Ok(new ResponseObjectDTO<dynamic>(titleResponse, [new { id = id.ToString(), fileUrl }]));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseMessageDTO(titleResponse, 500, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpPost("change-image")]
        public async Task<IActionResult> ChangeImageByUser(IFormFile file)
        {
            var titleResponse = "Change image employee.";
            try
            {
                // Get id of user request
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                //Generate token for authen server storage file
                var key = _tokenServ.GenerateAccessTokenImgServer();
                // Create file name
                var fileName = $"avatar-user-{userIdInToken}.{file.FileName.Split(".").Last()}";
                // Gọi service để upload file
                var fileUrl = await _staticFileServ.UploadImageAsync(file, fileName, key);
                // Update url image in database
                await _emplServ.UpdateImageAsync(long.Parse(userIdInToken), fileUrl);

                return Ok(new ResponseObjectDTO<dynamic>(titleResponse, [new { id = userIdInToken, fileUrl = $"{fileUrl}/{key}" }]));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseMessageDTO(titleResponse, 500, [ex.Message]));
            }
        }
    }
}
