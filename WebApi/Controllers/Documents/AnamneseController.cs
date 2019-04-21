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
    public class AnamneseController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly IMapper _mapper;
        private readonly AnamneseBL _bl;


        public AnamneseController(DBContext context, IHostingEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _mapper = mapper;
            _bl = new AnamneseBL();
        }

        // GET: api/Anamnese
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUser(int Id)
        {
            var item = _context.Anamnesen.Where(x => x.PatientId == Id)
                .Include(x => x.Chapters)
                .ThenInclude(y => y.Questions).ToList();

            QueryResponse<Anamnese> response = new QueryResponse<Anamnese>();
            response.Items = item;
            response.TotalRecords = 1;
            return Ok(response);
        }

        // PUT: api/Anamnese/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, Anamnese item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var _item = _context.Anamnesen
                .AsNoTracking()
                .Include(c => c.Chapters)
                .ThenInclude(q => q.Questions)
                .Where(m => m.Id == id)
                .ToList();
            if (_item == null)
            {
                return NoContent();
            }

            _mapper.Map(item, _item.FirstOrDefault());

            _bl.calculateCountPositivAnswers(_item.FirstOrDefault());

            _context.Anamnesen.Update(_item.FirstOrDefault());
            _context.SaveChanges();
            QueryResponse<Anamnese> response = new QueryResponse<Anamnese>();
            response.Items = _item;
            response.TotalRecords = _item.Count;
            return Ok(_item);
        }
    }
}
