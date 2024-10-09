using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelManagement.DTO;
using PersonnelManagement.Services;

namespace PersonnelManagement.Controllers
{
    public class AccountStatusController : Controller
    {
        private readonly IAccountStatusService _statusServ;

        public AccountStatusController(IAccountStatusService accountStatusService)
        {
            _statusServ = accountStatusService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AccountStatusDTO statusDTO)
        {
            var titleResponse = "Create a account status.";
            try
            {
                var status = await _statusServ.Add(statusDTO);
                return Ok(new ResponseObjectDTO<AccountStatusDTO>(titleResponse, [status]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] AccountStatusDTO statusDTO)
        {
            var titleResponse = "Update a account status.";
            try
            {
                var account = await _statusServ.Edit(statusDTO);
                return Ok(new ResponseObjectDTO<AccountStatusDTO>(titleResponse, [account]));
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
            var titleResponse = "Delete a account status.";
            try
            {
                await _statusServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete account id = {id} successfully."]));
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
            var titleResponse = "Get a account status.";
            try
            {
                var status = await _statusServ.Get(id);
                return Ok(new ResponseObjectDTO<AccountStatusDTO>(titleResponse, [status]));
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
            var titleResponse = "Get all account status.";
            try
            {
                var statuses = await _statusServ.GetAll();
                return Ok(new ResponseObjectDTO<AccountStatusDTO>(titleResponse, statuses, 1, 1, statuses.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
