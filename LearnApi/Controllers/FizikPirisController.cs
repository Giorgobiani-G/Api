﻿using System;
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
    [Route("v1/FizikPiris")]
    [ApiController]
    public class FizikPirisController : ControllerBase
    {
        private readonly CitizenDbContext _context;
        private readonly IStringLocalizer<FizikPirisController> _localizer;

        public FizikPirisController(CitizenDbContext context, IStringLocalizer<FizikPirisController> localizer)
        {
            _localizer = localizer;
            _context = context;
        }

        //Localization: Accept-Language Header : en-US, ka-GE

        //[HttpGet]
        //public string Get()
        //{
        //    return _localizer["Message"].Value;
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FizikPiri>>> GetFizikPiris(int pagenumber = 1, int pagesize = 4)
        {
            return await Pagination<FizikPiri>.CreateAsync(_context.FizikPiris.OrderBy(o => o.Saxeli).Include(inc => inc.ContactInfos)
                //.Include(inc=> inc.Image)
                .Include(inc => inc.ConnectedPersons), pagenumber, pagesize);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FizikPiri>> GetFizikPiri(int id)
        {
            var fizikPiri = await _context.FizikPiris.Where(x => x.FizikPiriId == id).
                Include(inc => inc.ContactInfos).
                //Include(inc => inc.Image).
                Include(inc => inc.ConnectedPersons).FirstOrDefaultAsync();

            if (fizikPiri == null)
            {
                return NotFound();
            }

            return fizikPiri;
        }

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
