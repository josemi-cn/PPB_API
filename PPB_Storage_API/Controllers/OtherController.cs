using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using PPB_Storage_API.Models;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace PPB_Storage_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OtherController : Controller
    {
        private readonly PpbStorageContext _context;

        public OtherController(PpbStorageContext context)
        {
            _context = context;
        }

        // GET: api/Commands
        [HttpGet("verifyIdentity")]
        public async Task<ActionResult<object>> GetIdentity()
        {
            return new { name = Environment.GetEnvironmentVariable("ENTERPRISE_NAME"), link = $"{Request.Scheme}://{Request.Host.Value}/", successful = true};
        }

        [HttpGet("Images/{filename}")]
        public IActionResult getImage(string filename)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            path = Path.Combine(path, filename);
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpeg");
        }

        [HttpPost("Images/upload")]
        public Task<object> uploadImage(IFormFile file)
        {
            bool result = false;

            string RutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "Images");
           
            if (!Directory.Exists(RutaCompleta))
            {
                Directory.CreateDirectory(RutaCompleta);
            }

            object o = new { successful = result };

            if (file.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string RutaFullCompleta = Path.Combine(RutaCompleta, fileName);

                using (var stream = new FileStream(RutaFullCompleta, FileMode.Create))
                {
                    file.CopyTo(stream);

                    result = true;
                    o = new { successful = result, filename = fileName };
                }

            }

            return Task.FromResult(o);
        }
    }
}
