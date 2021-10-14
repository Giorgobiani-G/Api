using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnApi.Models;
using Microsoft.Extensions.Localization;

namespace LearnApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FizikPirisController : ControllerBase
    {
        private readonly CitizenDbContext _context;
        private readonly IStringLocalizer<FizikPirisController> _localizer;

        public FizikPirisController(CitizenDbContext context, IStringLocalizer<FizikPirisController> localizer)
        {
            this._localizer = localizer;
            _context = context;
        }


        //Localization: Accept-Language Header : en-US, ka-GE

        //[HttpGet]
        //public string Get()
        //{
        //    return _localizer["Message"].Value;
        //}



        //GET: api/FizikPiris
       [HttpGet]
        public async Task<ActionResult<IEnumerable<FizikPiri>>> GetFizikPiris(int pagenumber = 1, int pagesize = 4)
        {

            //throw new Exception("TESt");
            return  await Pagination<FizikPiri>.CreateAsync(_context.FizikPiris.OrderBy(o => o.Saxeli).Include(inc => inc.ContactInfos)
                //.Include(inc=> inc.Image)
                .Include(inc => inc.ConnectedPersons), pagenumber, pagesize);


        }



        // GET: api/FizikPiris/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FizikPiri>> GetFizikPiri(int id)
        {
            var fizikPiri = await _context.FizikPiris.Where(x=>x.FizikPiriId==id).
                Include(inc => inc.ContactInfos).
                //Include(inc => inc.Image).
                Include(inc=>inc.ConnectedPersons).FirstOrDefaultAsync();

            if (fizikPiri == null)
            {
                return NotFound();
            }

            return fizikPiri;
        }

        // PUT: api/FizikPiris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFizikPiri(int id, FizikPiri fizikPiri)
        {
            if (id != fizikPiri.FizikPiriId)
            {
                return BadRequest();
            }

            _context.Entry(fizikPiri).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FizikPiriExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FizikPiris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FizikPiri>> PostFizikPiri(FizikPiri fizikPiri)
        {
            var varlidator = new FizikPiriValodator();
            var result = varlidator.Validate(fizikPiri);
            if (result.IsValid)
            {
                _context.FizikPiris.Add(fizikPiri);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetFizikPiri", new { id = fizikPiri.FizikPiriId }, fizikPiri);
            }
            return BadRequest(result.Errors);
            
        }

        // DELETE: api/FizikPiris/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFizikPiri(int id)
        {
            var fizikPiri = await _context.FizikPiris.FindAsync(id);
            if (fizikPiri == null)
            {
                return NotFound();
            }

            _context.FizikPiris.Remove(fizikPiri);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FizikPiriExists(int id)
        {
            return _context.FizikPiris.Any(e => e.FizikPiriId == id);
        }
    }
}
