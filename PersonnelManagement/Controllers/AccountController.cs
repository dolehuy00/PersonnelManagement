using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MovieAppApi.DTO;
using MovieAppApi.Service;
using PersonnelManagement.Data;
using PersonnelManagement.DTO;
using PersonnelManagement.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonnelManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private PersonnelDataContext _dataContext;
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _config;
        private readonly JwtTokenService _jwtTokenServ;
        private BuildJSONResponse _buildJSONResponse;

        public AccountController(PersonnelDataContext dataContext, IMemoryCache cache, IConfiguration config)
        {
            _dataContext = dataContext;
            _cache = cache;
            _config = config;
            _jwtTokenServ = new JwtTokenService();
            _buildJSONResponse = new BuildJSONResponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> CheckLogin([FromBody] RequestLoginDTO loginDTO)
        {
            try
            {
                var account = await _dataContext.Accounts
                    .Include(acc => acc.Employee)
                    .Include(acc => acc.Role)
                    .FirstOrDefaultAsync(acc => acc.Email.Equals(loginDTO.Email));
                if (account != null && loginDTO.Password.Equals(account.Password))
                {
                    var token = _jwtTokenServ.GenerateJwtToken(account, _config);
                    return Ok(_buildJSONResponse.LoginSuccessResponse(account, token));
                }
                return Unauthorized("Not match");
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

                var account = await _dataContext.Accounts
                    .FirstOrDefaultAsync(acc => acc.Email == changePassDTO.Email);
                if (account == null)
                {
                    return NotFound("This account is'nt exist!");
                }
                else if (changePassDTO.NewPassword.Length < 8)
                {
                    return BadRequest("Password must be at least 8 characters long!");
                }
                else if (changePassDTO.NewPassword != changePassDTO.PasswordConfirm)
                {
                    return BadRequest("Password confirm is not the same password");
                }
                else if (changePassDTO.OldPassword != account.Password)
                {
                    return BadRequest("Old password not match!");
                }
                else
                {
                    var userIdInToken = _jwtTokenServ.GetAccountIdFromToken(HttpContext);
                    if (int.Parse(userIdInToken) == account.Id)
                    {
                        account.Password = changePassDTO.NewPassword;
                        await _dataContext.SaveChangesAsync();
                        return Ok();
                    }
                    else
                    {
                        return Unauthorized("Can't change password!");
                    }

                }
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
                var existAccount = await _dataContext.Accounts
                    .Where(acc => acc.Email == forgotDTO.Email).FirstOrDefaultAsync() != null;
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
                var user = await _dataContext.Accounts.Where(u => u.Email == forgotDTO.Email).FirstOrDefaultAsync();
                var code = _cache.Get<ForgotPasswordConfirmCode>(forgotDTO.Email);
                var changeSuccess = false;
                if (user != null && code != null && code.CodeNumber.Equals(forgotDTO.Code))
                {
                    user.Password = forgotDTO.Password;
                    changeSuccess = await _dataContext.SaveChangesAsync() == 1;
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
