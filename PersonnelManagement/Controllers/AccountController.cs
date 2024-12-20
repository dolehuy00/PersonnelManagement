﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    public class AccountController : ControllerBase
    {
        private IAccountService _accServ;
        private readonly IMemoryCache _cache;
        private readonly TokenService _tokenServ;

        public AccountController(IMemoryCache cache, IAccountService accountService, TokenService tokenService)
        {
            _cache = cache;
            _accServ = accountService;
            _tokenServ = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> CheckLogin([FromBody] RequestLoginDTO loginDTO)
        {
            try
            {
                var account = await _accServ.ValidateUserAsync(loginDTO.Email, loginDTO.Password);
                if (account != null)
                {
                    var accessToken = _tokenServ.GenerateAccessToken(account.Id.ToString(), account.RoleName!);
                    var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                    var refreshToken = await _tokenServ.GenerateRefreshTokenAsync(account.Id.ToString(), account.RoleName!, ipAddress);
                    // Lưu Refresh Token vào HTTP-only Cookie
                    HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
                    {
                        HttpOnly = true, // Không cho phép truy cập qua JavaScript
                        Secure = true,   // Chỉ gửi qua HTTPS
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(7) // Thời gian sống
                    });
                    var response = new
                    {
                        account.Id,
                        accessToken,
                        account.Email,
                        account.EmployeeName,
                        EmployeeImage = account.EmployeeImage != null
                            ? $"{account.EmployeeImage}/{_tokenServ.GenerateAccessTokenImgServer()}" : null,
                        role = account.RoleName,
                        account.LeaderOfDepartments
                    };
                    return Ok(new ResponseObjectDTO<dynamic>("Login successfully", [response]));
                }
                return Unauthorized(
                    new ResponseMessageDTO("Can't login.", 401, ["Password or account is incorrect."]));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("get-access-token")]
        public async Task<IActionResult> GetAccessTokenUsingRefressToken()
        {
            var titleResponse = "Get access token.";
            try
            {
                // Lấy Refresh Token từ Cookie
                var refreshToken = HttpContext.Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                    return Ok(new ResponseMessageDTO(titleResponse, ["Refresh token is missing."]));
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                var (newAccessToken, newRefreshToken) = await _tokenServ.RefreshTokenAsync(refreshToken, ipAddress);

                // Lưu Refresh Token vào HTTP-only Cookie
                HttpContext.Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,   // Chỉ gửi qua HTTPS
                    SameSite = SameSiteMode.None,
                    Expires = DateTime.UtcNow.AddDays(7)
                });

                var response = new { newAccessToken };
                return Ok(new ResponseObjectDTO<dynamic>("Get access successfully.", [response]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [AllowAnonymous]
        [HttpPost("cancel-refressh-token")]
        public async Task<IActionResult> CancelRefressToken()
        {
            var titleResponse = "Cancel refressh token.";
            try
            {
                // Lấy Refresh Token từ Cookie
                var refreshToken = HttpContext.Request.Cookies["refreshToken"];
                if (string.IsNullOrEmpty(refreshToken))
                    return Ok(new ResponseMessageDTO(titleResponse, ["Refressh token is empty."]));
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var isCancel = await _tokenServ.CancelRefreshTokenAsync(refreshToken, userIdInToken);
                var response = isCancel ? "Refressh token canceled." : "Can not cancel refressh token.";
                return Ok(new ResponseMessageDTO(titleResponse, 200, [response]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AllRoles")]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] RequestChangePasswordDTO changePassDTO)
        {
            var titleResponse = "Change password.";
            try
            {
                if (changePassDTO.NewPassword.Length < 8)
                {
                    return BadRequest(new ResponseMessageDTO(
                        titleResponse, 400, ["Password must be at least 8 characters long."]));
                }
                if (changePassDTO.NewPassword != changePassDTO.PasswordConfirm)
                {
                    return BadRequest(new ResponseMessageDTO(
                        titleResponse, 400, ["Password confirm is not the same password."]));
                }
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                var result = await _accServ.ChangePasswordAsync(long.Parse(userIdInToken),
                    changePassDTO.CurrentPassword, changePassDTO.NewPassword);
                if (result)
                {
                    return Ok(new ResponseMessageDTO(titleResponse, ["Password changed successfully."]));
                }
                return BadRequest(new ResponseMessageDTO(
                    titleResponse, 400, ["Current password is incorrect or account not found."]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password-request")]
        public async Task<IActionResult> ForgotPasswordRequest([FromBody] ForgotPasswordDTO forgotDTO)
        {
            var titleResponse = "Forgot password.";
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
                            return BadRequest(new ResponseMessageDTO(titleResponse, 400,
                                ["Too many request, please wait 1 minute since the last successful request."]));
                        }
                    }
                    int randomCode = new Random().Next(100100, 999999);
                    _cache.Set(forgotDTO.Email, new ForgotPasswordCode(randomCode, DateTimeOffset.UtcNow.AddMinutes(5)),
                        TimeSpan.FromMinutes(5));
                    await new SMTPService().SendPasswordResetEmail(forgotDTO.Email, randomCode);
                    return Ok(new ResponseMessageDTO(titleResponse, ["Send request successfully."]));
                }
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, ["Account isn't exist."]));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password-verify-code")]
        public IActionResult ForgotPasswordVerifyCode([FromBody] ForgotPasswordDTO forgotDTO)
        {
            var titleResponse = "Forgot password verify code.";
            try
            {
                var code = _cache.Get<ForgotPasswordCode>(forgotDTO.Email);
                if (code != null && code.CodeNumber.Equals(forgotDTO.Code))
                {
                    code.IsVerified = true;
                    _cache.Set(forgotDTO.Email, code);
                    return Ok(new ResponseMessageDTO(titleResponse, ["Code verification successful."]));
                }
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, ["Code verification failed."]));
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("forgot-password-change")]
        public async Task<IActionResult> ForgotPasswordChange([FromBody] ForgotPasswordChangeDTO forgotDTO)
        {
            var titleResponse = "Forgot password change password.";
            try
            {
                if (forgotDTO.Password.Length < 8)
                {
                    return BadRequest(new ResponseMessageDTO(
                        titleResponse, 400, ["Password must be at least 8 characters long."]));
                }
                if (forgotDTO.Password != forgotDTO.PasswordConfirm)
                {
                    return BadRequest(new ResponseMessageDTO(
                        titleResponse, 400, ["Password confirm is not the same password."]));
                }
                var code = _cache.Get<ForgotPasswordCode>(forgotDTO.Email);
                if (code != null)
                {
                    if (!code.IsVerified)
                    {
                        return BadRequest(new ResponseMessageDTO(titleResponse, 400, ["Code not verified."]));
                    }
                    if (code.CodeNumber.Equals(forgotDTO.Code))
                    {
                        await _accServ.ChangePasswordNoCheckOldPassAsync(forgotDTO.Email, forgotDTO.Password);
                        return Ok(new ResponseMessageDTO(titleResponse, ["Change password successfully."]));
                    }
                }
                return BadRequest(new ResponseMessageDTO(
                    titleResponse, 400, ["Code has expired or account doesn't exist, please try again from step 1."]));
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [e.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AccountDTO accountDTO)
        {
            var titleResponse = "Create an account.";
            try
            {
                var account = await _accServ.Add(accountDTO);
                return Ok(new ResponseObjectDTO<AccountDTO>(titleResponse, [account]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] AccountDTO accountDTO)
        {
            var titleResponse = "Update an account.";
            try
            {
                var account = await _accServ.Edit(accountDTO);
                return Ok(new ResponseObjectDTO<AccountDTO>(titleResponse, [account]));
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
            var titleResponse = "Delete an account.";
            try
            {
                await _accServ.Delete(id);
                return Ok(new ResponseMessageDTO(titleResponse, [$"Delete account id = {id} successfully."]));
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
            var titleResponse = "Delete many account.";
            try
            {
                var messages = await _accServ.DeleteMany(id);
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
            var titleResponse = "Get an account.";
            try
            {
                var account = await _accServ.Get(id);
                return Ok(new ResponseObjectDTO<AccountDTO>(titleResponse, [account]));
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
            var titleResponse = "Get all account.";
            try
            {
                var accounts = await _accServ.GetAll();
                return Ok(new ResponseObjectDTO<AccountDTO>(titleResponse, accounts, 1, 1, accounts.Count));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] AccountFilterDTO filterDTO)
        {
            var titleResponse = "Filter account.";
            try
            {
                var (results, totalPage, totalRecords) = await _accServ.FilterAsync(filterDTO);
                return Ok(new ResponseObjectDTO<AccountDTO>(titleResponse, results, filterDTO.Page, totalPage, totalRecords));
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
            var titleResponse = "Lock account.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock your account");
                await _accServ.Lock(id);
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
            var titleResponse = "Unlock account.";
            try
            {
                var userIdInToken = _tokenServ.GetAccountIdFromAccessToken(HttpContext);
                if (userIdInToken == id.ToString()) throw new Exception("You can't lock/unlock your account");
                await _accServ.UnLock(id);
                return Ok(new ResponseMessageDTO(titleResponse, 200, [id.ToString(), Status.Active]));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseMessageDTO(titleResponse, 400, [ex.Message]));
            }
        }
    }
}
