using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LearnApi.Models;

namespace LearnApi.Controllers
{
 
    [Route("v1/ConnectedPersons")]
    [ApiController]
    public class ConnectedPersonsController : ControllerBase
    {
        private readonly CitizenDbContext _context;

        public ConnectedPersonsController(CitizenDbContext context)
        {
            _context = context;
        }

        // GET: api/ConnectedPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConnectedPerson>>> GetConnectedPersons()
        {
            return await _context.ConnectedPersons.ToListAsync();
        }

        // GET: api/ConnectedPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConnectedPerson>> GetConnectedPerson(int id)
        {
            var connectedPerson = await _context.ConnectedPersons.FindAsync(id);

            if (connectedPerson == null)
            {
                return NotFound();
            }

            return connectedPerson;
        }

        // PUT: api/ConnectedPersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutConnectedPerson(int Id, ConnectedPerson connectedPerson)
        {
            if (Id != connectedPerson.ConnectedPersonId)
            {
                return BadRequest();
            }

            _context.Entry(connectedPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConnectedPersonExists(Id))
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

        // POST: api/ConnectedPersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ConnectedPerson>> PostConnectedPerson(ConnectedPerson connectedPerson)
        {
            _context.ConnectedPersons.Add(connectedPerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConnectedPerson", new { id = connectedPerson.ConnectedPersonId }, connectedPerson);
        }

        // DELETE: api/ConnectedPersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConnectedPerson(int id)
        {
            var connectedPerson = await _context.ConnectedPersons.FindAsync(id);
            if (connectedPerson == null)
            {
                return NotFound();
            }

            _context.ConnectedPersons.Remove(connectedPerson);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConnectedPersonExists(int id)
        {
            return _context.ConnectedPersons.Any(e => e.ConnectedPersonId == id);
        }
    }
}
