using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Filters;

namespace PlatformDemo.Controllers
{
    [ApiKeyAuthFilter]
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]

    public class TicketsController : ControllerBase
    {
        private readonly BugTrackerContext _db;

        public TicketsController(BugTrackerContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = ticket.TicketId },
                value: ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            _db.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (_db.Tickets.Find(id) == null)
                    return NotFound();

                throw;  // generate a Internal Server Error 500
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            _db.Tickets.Remove(ticket);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}
