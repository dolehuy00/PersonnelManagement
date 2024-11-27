using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StorageStaticFileApplication.Services
{
    public class AuthenTokenService
    {
        private readonly IConfiguration _config;

        public AuthenTokenService(IConfiguration config)
        {
            _config = config;
        }

        public bool ValidateToken(string token, out ClaimsPrincipal? claims)
        {
            claims = null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JWTAccessTokenImgServer:Key"]!);

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                claims = tokenHandler.ValidateToken(token, validationParameters, out _);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
