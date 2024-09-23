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
                        return Unauthorized();
                    }

                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
