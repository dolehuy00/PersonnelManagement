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
                if (account != null && loginDTO.Password.Equals(account.Password))
                {
                    var token = _jwtTokenServ.GenerateJwtToken(account, _config);
                    return Ok(_jsonResponseServ.LoginSuccessResponse(account, token));
                }
                return Unauthorized(new { message = "Password or account is incorrect!" });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] RequestChangePasswordDTO changePassDTO)
        {
            try
            {
                if (changePassDTO.NewPassword.Length < 8)
                {
                    return BadRequest("Password must be at least 8 characters long!");
                }
                else if (changePassDTO.NewPassword != changePassDTO.PasswordConfirm)
                {
                    return BadRequest("Password confirm is not the same password");
                }
                var userIdInToken = _jwtTokenServ.GetAccountIdFromToken(HttpContext);
                var result = await _accServ.ChangePasswordAsync(long.Parse(userIdInToken),
                    changePassDTO.CurrentPassword, changePassDTO.NewPassword);
                if (result)
                {
                    return Ok("Password changed successfully");
                }
                return BadRequest("Current password is incorrect or account not found");
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
                    var code = _cache.Get<ForgotPasswordConfirmCode>(forgotDTO.Email);
                    if (code != null)
                    {
                        if (DateTimeOffset.UtcNow.AddMinutes(4) < code.DeadTime)
                        {
                            return BadRequest("Too many request, please wait 1 minute since the last successful request");
                        }
                    }
                    int randomCode = new Random().Next(100100, 999999);
                    _cache.Set(forgotDTO.Email, new ForgotPasswordConfirmCode(randomCode, DateTimeOffset.UtcNow.AddMinutes(5)), TimeSpan.FromMinutes(5));
                    await new SMTPService().SendPasswordResetEmail(forgotDTO.Email, randomCode);
                }
                return Ok(new { existAccount });
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
                var code = _cache.Get<ForgotPasswordConfirmCode>(forgotDTO.Email);
                var codeMatch = code != null && code.CodeNumber.Equals(forgotDTO.Code);
                return Ok(new { codeMatch });
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
                    return BadRequest("Password must be at least 8 characters long");
                }
                else if (forgotDTO.Password != forgotDTO.PasswordConfirm)
                {
                    return BadRequest("Password confirm is not the same password");
                }
                var code = _cache.Get<ForgotPasswordConfirmCode>(forgotDTO.Email);
                var changeSuccess = false;
                if (code != null && code.CodeNumber.Equals(forgotDTO.Code))
                {
                    changeSuccess = await _accServ.ChangePasswordAsync(forgotDTO.Email,
                        forgotDTO.Password, forgotDTO.PasswordConfirm);
                }
                return Ok(new { changeSuccess });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("test-role-admin")]
        public IActionResult TestRoleAdmin()
        {
            return Ok("Welcome Admin");
        }

        [Authorize(Roles = "User")]
        [HttpGet("test-role-user")]
        public IActionResult TestRoleUser()
        {
            return Ok("Welcome User");
        }

        [Authorize]
        [HttpGet("test-role-all")]
        public IActionResult TestRoleAll()
        {
            return Ok("ok");
        }
    }
}
