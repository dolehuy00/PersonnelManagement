using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonnelManagement.DTO;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieAppApi.Service
{
    public class TokenService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IConfiguration _config;

        public TokenService(IConnectionMultiplexer redis, IConfiguration config)
        {
            _redis = redis;
            _config = config;
        }

        public string GetAccountIdFromAccessToken(HttpContext context)
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

        public string GenerateAccessToken(string id, string roleName)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _config["AccessTokenJwt:Subject"]!),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", id),
                        new Claim(ClaimTypes.Role, roleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AccessTokenJwt:Key"]!));
            var signIng = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["AccessTokenJwt:Issuer"],
                _config["AccessTokenJwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["AccessTokenJwt:ExpiresMinutes"]!)),
                signingCredentials: signIng);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshTokenAsync(string userId, string roleName, string? ipAddress)
        {
            var token = Guid.NewGuid().ToString();

            var refreshToken = new RefreshToken
            {
                Token = token,
                UserId = userId,
                RoleName = roleName,
                IpAddress = ipAddress,
                ExpiryDate = DateTime.UtcNow.Add(TimeSpan.FromDays(7))
            };

            // Lưu trữ Refresh Token vào Redis, với khóa là Token
            var db = _redis.GetDatabase();
            var key = $"refresh_token:{token}";
            await db.StringSetAsync(key, JsonConvert.SerializeObject(refreshToken), TimeSpan.FromDays(7));

            return token;
        }

        public async Task<(bool, RefreshToken?)> ValidateRefreshTokenAsync(string token, string? ipAddress)
        {
            var db = _redis.GetDatabase();
            var key = $"refresh_token:{token}";
            var storedTokenJson = await db.StringGetAsync(key);

            if (storedTokenJson.IsNullOrEmpty)
            {
                return (false, null);
            }

            var refreshToken = JsonConvert.DeserializeObject<RefreshToken>(storedTokenJson.ToString());

            // Kiểm tra xem IP có khớp hay không
            if (refreshToken == null || refreshToken.IpAddress != ipAddress)
            {
                return (false, null);
            }

            // Kiểm tra xem Token có hết hạn hay không
            if (refreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return (false, null);
            }

            // Xóa token cũ để ngăn tái sử dụng
            await db.KeyDeleteAsync(key);

            return (true, refreshToken);
        }

        public async Task<(string newAccessToken, string newRefreshToken)> RefreshTokenAsync(string token, string? ipAddress)
        {
            var (isValid, refreshToken) = await ValidateRefreshTokenAsync(token, ipAddress);
            if (!isValid || refreshToken == null)
            {
                throw new UnauthorizedAccessException("Invalid refresh token.");
            }
            var newAccessToken = GenerateAccessToken(refreshToken.UserId, refreshToken.RoleName);

            // Tạo refresh token mới
            var newRefreshToken = await GenerateRefreshTokenAsync(refreshToken.UserId, refreshToken.RoleName, ipAddress);

            return (newAccessToken, newRefreshToken);
        }

        public async Task<bool> CancelRefreshTokenAsync(string token, string userId)
        {
            var db = _redis.GetDatabase();
            var key = $"refresh_token:{token}";
            var storedTokenJson = await db.StringGetAsync(key);

            if (storedTokenJson.IsNullOrEmpty)
            {
                return true;
            }

            var refreshToken = JsonConvert.DeserializeObject<RefreshToken>(storedTokenJson.ToString());

            // Kiểm tra Token
            if (refreshToken == null || refreshToken.UserId == userId)
            {
                // Xóa token
                await db.KeyDeleteAsync(key);
                return true;
            }

            return false;
        }

    }
}

