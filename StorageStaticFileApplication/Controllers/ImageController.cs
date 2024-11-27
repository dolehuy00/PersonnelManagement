using Microsoft.AspNetCore.Mvc;
using StorageStaticFileApplication.Services;

namespace StorageStaticFileApplication.Controllers
{
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AuthenTokenService _tokenServ;

        public ImageController(IWebHostEnvironment env, AuthenTokenService tokenServ)
        {
            _env = env;
            _tokenServ = tokenServ;
        }

        [HttpGet("{fileName}/{key}")]
        public IActionResult GetImageFile(string fileName, string key)
        {
            // Xác thực token
            if (!_tokenServ.ValidateToken(key, out _))
            {
                return Unauthorized();
            }

            var filePath = Path.Combine(_env.ContentRootPath, "Uploads", fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }

            var contentType = "application/octet-stream";
            return PhysicalFile(filePath, contentType, fileName);
        }
    }
}
