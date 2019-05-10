using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using kubaapi.utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rl_bl.Context;
using rl_contract.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileDataController : Controller
    {
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };
        private readonly IHostingEnvironment host;
        private readonly DBContext _context;

        public FileDataController(DBContext context, IHostingEnvironment environment)
        {
            _context = context;
            host = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery(Name = "filename")] string filename)
        {
            var path = Path.Combine(host.WebRootPath, "uploads", filename);
            var imageFileStream = System.IO.File.OpenRead(path);

            return File(imageFileStream, "image/jpeg");
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile filesData)
        {
            if (filesData == null) return BadRequest("Null File");
            if (filesData.Length == 0)
            {
                return BadRequest("Empty File");
            }
            if (filesData.Length > 10 * 1024 * 1024) return BadRequest("Max file size exceeded.");
            if (!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(filesData.FileName).ToLower())) return BadRequest("Invalid file type.");
            var uploadFilesPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(filesData.FileName);
            var filePath = Path.Combine(uploadFilesPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await filesData.CopyToAsync(stream);
            }
            var photo = new FileData { FileName = fileName };
            _context.FileDatas.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { name = fileName }, photo);
        }
    }
}