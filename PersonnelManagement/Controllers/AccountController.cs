using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.Service;
using PersonnelManagement.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountService _accServ;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly JwtTokenService _jwtTokenServ;
        private JsonResponseService _jsonResponseServ;

        public AccountController(IMemoryCache cache, IConfiguration config, IAccountService accountService)
        {
            _cache = cache;
            _config = config;
            _accServ = accountService;
            _jwtTokenServ = new JwtTokenService();
            _jsonResponseServ = new JsonResponseService();
        }

        [HttpPost("login")]
        public async Task<IActionResult> CheckLogin([FromBody] RequestLoginDTO loginDTO)
        {
            try
            {
                var account = await _accServ.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
                if (account != null)
                {
                    var token = _jwtTokenServ.GenerateJwtToken(account, _config);
                    return Ok(_jsonResponseServ.LoginSuccessResponse(account, token));
                }
                return Unauthorized(_jsonResponseServ.LoginNotMatchResponse());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] RequestChangePasswordDTO changePassDTO)
        {
            try
            {
                if (changePassDTO.NewPassword != changePassDTO.PasswordConfirm)
                {
                    return BadRequest(_jsonResponseServ.BadMessageResponse(
                        "Change password.", ["Password confirm is not the same password."]));
                }
                var userIdInToken = _jwtTokenServ.GetAccountIdFromToken(HttpContext);
                var result = await _accServ.ChangePasswordAsync(long.Parse(userIdInToken),
                    changePassDTO.CurrentPassword, changePassDTO.NewPassword);
                if (result)
                {
                    return Ok(_jsonResponseServ.OkMessageResponse(
                        "Change password.", ["Password changed successfully."]));
                }
                return BadRequest(_jsonResponseServ.BadMessageResponse(
                        "Change password.", ["Current password is incorrect or account not found."]));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("forgot-password-request")]
        public async Task<IActionResult> ForgotPasswordRequest([FromBody] ForgotPasswordDTO forgotDTO)
        {
            try
            {
                var existAccount = await _accServ.ExistAccountAsync(forgotDTO.Email);
                if (existAccount)
                {
                    var code = _cache.Get<ForgotPasswordCode>(forgotDTO.Email);
                    if (code != null)
                    {
                        if (DateTimeOffset.UtcNow.AddMinutes(4) < code.DeadTime)
                        {
                            return BadRequest(_jsonResponseServ.BadMessageResponse("Forgot password.",
                                ["Too many request, please wait 1 minute since the last successful request."]));
                        }
                    }
                    int randomCode = new Random().Next(100100, 999999);
                    _cache.Set(forgotDTO.Email, new ForgotPasswordCode(randomCode, DateTimeOffset.UtcNow.AddMinutes(5)),
                        TimeSpan.FromMinutes(5));
                    await new SMTPService().SendPasswordResetEmail(forgotDTO.Email, randomCode);
                    return Ok(_jsonResponseServ.OkMessageResponse("Forgot password.", ["Send request successfully."]));
                }
                return Ok(_jsonResponseServ.BadMessageResponse("Forgot password.", ["Account isn't exist."]));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("forgot-password-checkcode")]
        public IActionResult ForgotPasswordCheckCode([FromBody] ForgotPasswordDTO forgotDTO)
        {
            try
            {
                var code = _cache.Get<ForgotPasswordCode>(forgotDTO.Email);
                if (code != null && code.CodeNumber.Equals(forgotDTO.Code))
                {
                    return Ok(_jsonResponseServ.OkMessageResponse("Forgot password.", ["Code verification successful."]));
                }
                return BadRequest(_jsonResponseServ.BadMessageResponse("Forgot password.", ["Code verification failed."]));
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpPost("forgot-password-change")]
        public async Task<IActionResult> ForgotPasswordChange([FromBody] ForgotPasswordDTO forgotDTO)
        {
            try
            {
                if (forgotDTO.Password == null || forgotDTO.Password.Length < 8)
                {
                    return BadRequest(_jsonResponseServ.BadMessageResponse("Forgot password.",
                        ["Password must be at least 8 characters long."]));
                }
                else if (forgotDTO.Password != forgotDTO.PasswordConfirm)
                {
                    return BadRequest(_jsonResponseServ.BadMessageResponse("Forgot password.",
                        ["Password confirm is not the same password."]));
                }
                var code = _cache.Get<ForgotPasswordCode>(forgotDTO.Email);
                var changeSuccess = false;
                if (code != null && code.CodeNumber.Equals(forgotDTO.Code))
                {
                    changeSuccess = await _accServ.ChangePasswordAsync(forgotDTO.Email,
                        forgotDTO.Password, forgotDTO.PasswordConfirm);
                }
                return Ok(_jsonResponseServ.OkMessageResponse("Forgot password.", ["Change password successfully."]));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("add")]
        public async Task<IActionResult> Add([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var account = await _accServ.Add(accountDTO);
                return Ok(_jsonResponseServ.OkOneAccountResponse("Create an account.", [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Create an account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("edit")]
        public async Task<IActionResult> Edit([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var account = await _accServ.Edit(accountDTO);
                return Ok(_jsonResponseServ.OkOneAccountResponse("Update an account.", [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Update an account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _accServ.Delete(id);
                return Ok(_jsonResponseServ.OkMessageResponse("Delete an account.", [$"Delete account id = {id} successfully."]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Delete an account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("delete")]
        public async Task<IActionResult> DeleteMany([FromBody] long[] ids)
        {
            try
            {
                var messages = await _accServ.DeleteMany(ids);
                return Ok(_jsonResponseServ.OkMessageResponse("Delete many account.", messages));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Delete many account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var account = await _accServ.Get(id);
                return Ok(_jsonResponseServ.OkOneAccountResponse("Get an account.", [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get an account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/{page}/{itemPerPage}")]
        public async Task<IActionResult> Get(int page, int itemPerPage)
        {
            try
            {
                var (accounts, totalPage, totalRecords) = await _accServ.GetPagedListWithTotalPagesAsync(page, itemPerPage);
                return Ok(_jsonResponseServ.OkListAccountResponse("Get page account.", accounts, page, totalPage, totalRecords));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get page account.", [ex.Message]));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var accounts = await _accServ.GetAll();
                return Ok(_jsonResponseServ.OkListAccountResponse("Get all account.", accounts, 1, 1, accounts.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(_jsonResponseServ.BadMessageResponse("Get all account.", [ex.Message]));
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
                return BadRequest(_jsonResponseServ.BadMessageResponse("Filter account.", [ex.Message]));
            }
        }
    }
}
