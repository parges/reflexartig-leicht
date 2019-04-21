using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rl_bl.Context;

namespace kuba_api.Controllers
{
    [Produces("application/json")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        private string _imagePath;

        // Constructor
        public ImageController(DBContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _imagePath = _environment.ContentRootPath + @"\Resources\Images\";
        }

        /*[HttpGet]
        public string Get([FromBody] string filename)
        {
            /*var patient = _context.Patients.Where(x => x.Avatar == filename);#1#

            return "ok";
        }*/

        [HttpGet("{id}", Name = "GetImage")]
        public ActionResult GetImage(int id)
        {
            var patient = _context.Patients.Find(id);
            if (!string.IsNullOrEmpty(patient.Avatar))
            {
                var bytes = System.IO.File.ReadAllBytes(_imagePath + patient.Avatar);
                return File(bytes, "application/image");
            }

            return NotFound();
        }

    }
}