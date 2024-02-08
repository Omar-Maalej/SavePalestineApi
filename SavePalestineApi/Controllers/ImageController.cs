using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SavePalestineApi.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string _imagesFolderPath;

        public ImageController()
        {
            _imagesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        }

        [HttpGet("{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var filePath = Path.Combine(_imagesFolderPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileStream = System.IO.File.OpenRead(filePath);
            return File(fileStream, "image/jpeg"); 
        }
    }
}
