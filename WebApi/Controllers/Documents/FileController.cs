using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using kubaapi.utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rl_bl;
using rl_bl.Context;
using rl_contract.Models;

namespace kuba_api.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        
        private Dictionary<int, string> pdfLinks = new Dictionary<int, string>
        {
            {1, "INPPInternationalTestformular.pdf"},
            {2, "Fragebogen - Kinder geändert.pdf"},
            {3, "INPPInternationalTestformular.pdf"},
            {4, "INPPInternationalTestformular.pdf"},
            {5, "INPPInternationalTestformular.pdf"},
        };

        public FileController(DBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            
        }

        // GET: api/Document
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int Id)
        {
            string webRootPath = _environment.WebRootPath;
            string contentRootPath = _environment.ContentRootPath;

            var stream = new FileStream(webRootPath+ @"\Documents\"+ pdfLinks[Id], FileMode.Open);
            /*return new FileStreamResult(stream, "application/pdf");*/

            return File(stream, "application/pdf");

            /*return new FileStream(webRootPath + @"\Documents\INPPInternationalTestformular.pdf", FileMode.Open, FileAccess.Read);*/

            /*try
            {
                string file = webRootPath + @"\Documents\INPPInternationalTestformular.pdf";

                var memory = new MemoryStream();
                using (var stream = new FileStream(file, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }

                memory.Position = 0;
                return File(memory, GetMimeType(file), "INPPInternationalTestformular.pdf");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }*/


        }

        private string GetMimeType(string file)
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            switch (extension)
            {
                case ".txt": return "text/plain";
                case ".pdf": return "application/pdf";
                case ".doc": return "application/vnd.ms-word";
                case ".docx": return "application/vnd.ms-word";
                case ".xls": return "application/vnd.ms-excel";
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".gif": return "image/gif";
                case ".csv": return "text/csv";
                default: return "";
            }
        }

    }
}
