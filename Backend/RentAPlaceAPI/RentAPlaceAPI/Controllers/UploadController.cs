using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentAPlaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Allow only image types
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg", "image/gif" };
            if (!allowedTypes.Contains(file.ContentType.ToLower()))
                return BadRequest("Only image files are allowed (jpg, png, gif).");

            // Save to Uploads folder in project root
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Keep original filename
            var fileName = file.FileName;
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Return full URL so Angular can use it directly
            var fileUrl = $"https://localhost:7287/Uploads/{fileName}";
            return Ok(new { path = "/Uploads/" + fileName, url = fileUrl });
        }
    }
}
