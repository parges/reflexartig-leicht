using System;
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
    [ApiController]
    [Consumes("application/json")]
    public class TestungController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly TestungBL _bl;


        public TestungController(DBContext context, IHostingEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _mapper = mapper;
            _bl = new TestungBL();
        }

    // GET: api/Documents
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTestungByUser(int Id)
    {
        var testung = _context.Testungen.Where(x => x.PatientId == Id)
            .Include(x => x.Chapters)
            .ThenInclude(y => y.Questions).ToList();

        QueryResponse<Testung> response = new QueryResponse<Testung>();
        response.Items = testung;
        response.TotalRecords = 1;
        return Ok(response);
    }

    // PUT: api/Patient/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, Testung item)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var testung = _context.Testungen
            .AsNoTracking()
            .Include(c => c.Chapters)
            .ThenInclude(q => q.Questions)
            .Where(m => m.Id == id)
            .ToList();
        if (testung == null)
        {
            return NoContent();
        }

        _mapper.Map(item, testung.FirstOrDefault());

        _bl.calculateScore(testung.FirstOrDefault());

        _context.Testungen.Update(testung.FirstOrDefault());
        _context.SaveChanges();
        QueryResponse<Testung> response = new QueryResponse<Testung>();
        response.Items = testung;
        response.TotalRecords = testung.Count;
        return Ok(testung);
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
