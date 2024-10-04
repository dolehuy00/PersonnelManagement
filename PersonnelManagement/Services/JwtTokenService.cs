
using Microsoft.IdentityModel.Tokens;
using PersonnelManagement.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MovieAppApi.Service
{
    public class JwtTokenService
    {
        public string GetAccountIdFromToken(HttpContext context)
        {
            // Lấy token từ tiêu đề Authorization của HttpContext
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                // Giải mã token thành một đối tượng JwtSecurityToken
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    // Lấy các claims từ token
                    var claims = jwtToken.Claims;

                    // Tìm claim có tên là "Id" và lấy giá trị của nó
                    var userIdClaim = claims.FirstOrDefault(c => c.Type == "Id");

                    if (userIdClaim != null)
                    {
                        return userIdClaim.Value;
                    }
                }
            }
            return string.Empty;
        }
        public string GenerateJwtToken(AccountDTO account, IConfiguration _config)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", account.Id.ToString()),
                        new Claim("Email", account.Email),
                        new Claim(ClaimTypes.Role, account.RoleName!)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var signIng = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signIng);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}

