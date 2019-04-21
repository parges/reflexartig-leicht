using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using kubaapi.utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using rl_bl;
using rl_bl.Context;
using rl_contract.Models;
using rl_contract.Models.Review;

namespace kuba_api.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly PatientBL _bl;
        private readonly IMapper _mapper;

        public ReviewController(DBContext context, IHostingEnvironment environment, IMapper mapper)
        {
            _context = context;
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _bl = new PatientBL();
            _mapper = mapper;
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = _context.Reviews.AsNoTracking().Where(x => x.Id == id)
                        .Include(x => x.Chapters)
                        .ThenInclude(q => q.Questions)
                        .Include(x=> x.ProblemHierarchies)
                        .ToList();

            if (item.FirstOrDefault() == null)
            {
                return NotFound();
            }

            QueryResponse<Review> response = new QueryResponse<Review>();
            response.Items = item;
            response.TotalRecords = 1;

            return Ok(response);
        }

        // POST: api/Patient
        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (patient == null)
            {
                return BadRequest(patient);
            }
            Review rev = _bl.addReview(patient);
            /*patient.Reviews.Add(rev);*/
            _context.Reviews.Add(rev);
            await _context.SaveChangesAsync();

            return Created($"/{ControllerContext.ActionDescriptor.ControllerName}/{rev?.Id}", rev);
        }


        // PUT: api/Patient/5
        [HttpPut("{id}")]
        [ActionName("UpdateReview")]
        public async Task<IActionResult> Update([FromRoute] int id, Review item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var review = _context.Reviews.AsNoTracking().Where(x => x.Id == id).Include(x => x.ProblemHierarchies).Include(x => x.Chapters).FirstOrDefault(m => m.Id == id);
            if (review == null)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(item, review);
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return Ok(review);
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        [ActionName("UpdateReviewQuestions")]
        public async Task<IActionResult> UpdateReviewQuestions([FromRoute] int id, [FromBody]int[] chosenIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var review = _context.Reviews.AsNoTracking().Where(x => x.Id == id).Include(x => x.ProblemHierarchies).Include(x => x.Chapters).ThenInclude(c => c.Questions).FirstOrDefault(m => m.Id == id);
            if (review == null)
            {
                return BadRequest(ModelState);
            }

            IList<int> list = chosenIds.ToList();
            var questions = _context.TestungQuestions.Where(x => list.IndexOf(x.Id.Value) >= 0).ToList();
            var chapterIds = _context.TestungQuestions.Where(x => list.IndexOf(x.Id.Value) >= 0).Select(c => c.TestungChapterId).Distinct();
            var chapters = _context.TestungChapters.Where(x => chapterIds.IndexOf(x.Id.Value) >= 0).ToList();

            _bl.addReviewTests(review, chapters, _context);

            var reviewUpdated = _context.Reviews.AsNoTracking().Where(x => x.Id == id).Include(x => x.ProblemHierarchies).Include(x => x.Chapters).ThenInclude(c=>c.Questions).FirstOrDefault(m => m.Id == id);

            return Ok(reviewUpdated);

        }



        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = _context.Reviews.AsNoTracking().Where(x=>x.Id == id).Include(x=>x.ProblemHierarchies).ToList();
            if (item == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(item.FirstOrDefault());
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ActionName("DeleteTests")]
        public async Task<IActionResult> DeleteTests(int id)
        {
            var item = _context.ReviewChapters.AsNoTracking().Where(x => x.ReviewId == id).Include(x => x.Questions).ToList();
            if (item == null)
            {
                return NotFound();
            }
            item.ForEach(chapter =>
            {
                chapter.Questions.ForEach(question => { _context.ReviewQuestion.Remove(question); });
                _context.ReviewChapters.Remove(chapter);
            });
            /*_context.Reviews.Remove(item.FirstOrDefault());*/
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
