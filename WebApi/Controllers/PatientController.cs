using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class PatientController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        /*private readonly ImageUploader.Helper.IImageHandler _imageHandler;*/
        private readonly PatientBL _bl;
        private readonly IMapper _mapper;

        private string _imagePath;

        public PatientController(DBContext context, IHostingEnvironment environment, /*ImageUploader.Helper.IImageHandler imageHandler, */IMapper mapper)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            /*_imageHandler = imageHandler;*/
            _imagePath = _environment.ContentRootPath + @"\Resources\Images";
            _bl = new PatientBL();
            _mapper = mapper;
        }

        // GET: api/Patient
        [HttpGet]
        [ActionName("GetAll")]
        public async Task<IActionResult> Get()
        {
            List<Patient> list = _context.Patients.Include(x => x.Reviews).ToList();
            // Order by Date ASC
            list.ForEach(x =>
            {
                x.Reviews = x.Reviews.OrderBy(y => y.Date).ToList();
            });
            QueryResponse<Patient> response = new QueryResponse<Patient>();
            response.Items = list;
            response.TotalRecords = list.Count;
            return Ok(response);
        }

        [HttpGet]
        [ActionName("GetAllDebtors")]
        public async Task<IActionResult> GetAllDebtors()
        {
            List<Patient> list = _context.Patients.Include(x => x.Reviews)
                .Where(x=> x.AnamnesePayed == false || x.DiagnostikPayed == false || x.ElternPayed == false || x.Reviews.Any(y=> y.Payed==false))
                .ToList();
            QueryResponse<Patient> response = new QueryResponse<Patient>();
            response.Items = list;
            response.TotalRecords = list.Count;
            return Ok(response);
        }

        // GET: api/Patient/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = _context.Patients.Where(x => x.Id == id).Include(x => x.Reviews).ToList();
                /*.Include(p => p.Reviews)*/
                /*.Find(id);*/
            if (item.FirstOrDefault() == null)
            {
                return NotFound();
            }

            /*PatientDto patient = new PatientDto();
            patient.Id = item.Id;
            patient.Firstname = item.Firstname;
            patient.Lastname = item.Lastname;
            patient.Birthday = item.Birthday;
            patient.Tele = item.Tele;
            /*var image = System.IO.File.OpenRead(_imagePath + );#1#
            patient.Avatar = item.Avatar;*/

            // Order by Date ASC
            item.FirstOrDefault().Reviews = item.FirstOrDefault().Reviews.OrderBy(x => x.Date).ToList();

            QueryResponse<Patient> response = new QueryResponse<Patient>();
            response.Items = item;
            response.TotalRecords = 1;

            return Ok(response);
        }

        /*// GET: api/Patient/5
        [HttpGet("{filename}", Name = "GetImage")]
        public IActionResult GetImage(string filename)
        {
            var patient = _context.Patients.Where(x => x.Avatar == filename);

            var bytes = System.IO.File.ReadAllBytes(_imagePath + filename);

            return File(bytes, "application/image");

            /*PatientDto patient = new PatientDto();
            patient.Id = item.Id;
            patient.Firstname = item.Firstname;
            patient.Lastname = item.Lastname;
            patient.Birthday = item.Birthday;
            patient.Tele = item.Tele;
            /*var image = System.IO.File.OpenRead(_imagePath + );#2#
            patient.Avatar = item.Avatar;#1#

        }*/

        // POST: api/Patient
        [HttpPost]
        public async Task<IActionResult> Create(Patient item)
        {
            if (item == null)
            {
                return BadRequest(item);
            }
            _bl.addRelevantPatientData(item, _context);
            _context.Patients.Add(item);
            await _context.SaveChangesAsync();

            return Created($"/{ControllerContext.ActionDescriptor.ControllerName}/{item?.Id}", item);
        }

        // PUT: api/Patient/5
        /*[HttpPut("{id}")]
        public ActionResult UpdateAsync([FromRoute] int id, [FromForm] PatientDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _context.Patients.Where(x => x.Id == id).Include(x => x.Reviews).FirstOrDefault(m => m.Id == id);
            if (patient == null)
            {
                return NoContent();
            }
            patient.Firstname = item.Firstname;
            patient.Lastname= item.Lastname;
            patient.Birthday= item.Birthday;
            patient.Tele = item.Tele;
            patient.Reviews = item.Reviews;

            if (item.Avatar != null)
            {
                string filename = _imageHandler.UploadImage(item.Avatar, _imagePath).Result;
                patient.Avatar = filename;
            }
            /*var filePath = Path.Combine(_environment.ContentRootPath, @"Resources/Images", item.Avatar.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                item.Avatar.CopyTo(stream);
            }#1#

            _context.Patients.Update(patient);
            _context.SaveChanges();
            return Ok(patient);
        }*/

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, Patient item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = _context.Patients.AsNoTracking().Where(x => x.Id == id).Include(x => x.Reviews).FirstOrDefault(m => m.Id == id);
            if (patient == null)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(item, patient);

            /*if (item.Avatar != null)
            {
                string filename = _imageHandler.UploadImage(item.Avatar, _imagePath).Result;
                patient.Avatar = filename;
            }*/
            /*var filePath = Path.Combine(_environment.ContentRootPath, @"Resources/Images", item.Avatar.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                item.Avatar.CopyTo(stream);
            }*/

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return Ok(patient);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
        
    }
}
