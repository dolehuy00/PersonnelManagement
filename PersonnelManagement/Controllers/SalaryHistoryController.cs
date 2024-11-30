using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    public class SalaryHistoryController : ControllerBase
    {
        private readonly ISalaryHistoryService _sHServ;
        private readonly TokenService _tokenServ;

        public SalaryHistoryController(ISalaryHistoryService salaryHistoryService, TokenService tokenService)
        {
            _sHServ = salaryHistoryService;
            _tokenServ = tokenService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] SalaryHistoryDTO salaryHistoryDTO)
        {
            var titleResponse = "Create an Salary History.";
            try
            {
                var salaryHistory = await _sHServ.Add(salaryHistoryDTO);
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, [salaryHistory]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] SalaryHistoryDTO salaryHistoryDTO)
        {
            var titleResponse = "Update an Salary History.";
            try
            {
                var salaryHistory = await _sHServ.Edit(salaryHistoryDTO);
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, [salaryHistory]));
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
            var titleResponse = "Delete an Salary History.";
            try
            {
                await _sHServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete Salary History id = {id} successfully."]));
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
            var titleResponse = "Delete many Salary History.";
            try
            {
                var messages = await _sHServ.DeleteMany(id);
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
            var titleResponse = "Get an Salary History.";
            try
            {
                var salaryHistory = await _sHServ.Get(id);
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, [salaryHistory]));
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
            var titleResponse = "Get all Salary History.";
            try
            {
                var salaryHistories = await _sHServ.GetAll();
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, salaryHistories, 1, 1, salaryHistories.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] SalaryHistoryFilterDTO filterDTO)
        {
            var titleResponse = "Filter Salary History.";
            try
            {
                var (results, totalPage, totalRecords) = await _sHServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
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
            var titleResponse = "Get an Salary History.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var salaryHistory = await _sHServ.GetByEmployee(id, long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, [salaryHistory]));
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
            var titleResponse = "Get page Salary History.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var (salaryHistories, totalPage, totalRecords) = await _sHServ.GetPagesByEmployeeAsync(page, itemPerPage, long.Parse(userIdInToken));
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, salaryHistories, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpGet("filter-by-user")]
        public async Task<IActionResult> FilterByUser([FromQuery] SalaryHistoryFilterDTO filterDTO)
        {
            var titleResponse = "Filter Salary History.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == null)
                {
                    return Unauthorized(new ResponseMessageDTO(titleResponse, 401, ["Permissions denied."]));
                }
                filterDTO.EmployeeId = long.Parse(userIdInToken);
                var (results, totalPage, totalRecords) = await _sHServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<SalaryHistoryDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
