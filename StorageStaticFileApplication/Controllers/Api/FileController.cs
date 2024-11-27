using Microsoft.AspNetCore.Mvc;
using StorageStaticFileApplication.Services;

namespace StorageStaticFileApplication.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly AuthenTokenService _tokenServ;

        public FileController(IWebHostEnvironment env, AuthenTokenService tokenServ)
        {
            _env = env;
            _tokenServ = tokenServ;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromForm] string key)
        {
            // Xác thực token
            if (!_tokenServ.ValidateToken(key, out _))
            {
                return Unauthorized();
            }

            if (file == null || file.Length == 0)
            {
                return BadRequest("No file provided.");
            }

            if (file.Length > 5 * 1024 * 1024) // 5 MB
            {
                return BadRequest("File is too large.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(fileExtension.ToLower()))
            {
                return BadRequest("File type not allowed.");
            }

            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/image/{file.FileName}";
            return Ok(new { file.FileName, FileUrl = fileUrl });
        }
    }
}
