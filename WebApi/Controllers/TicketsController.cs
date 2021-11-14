using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PlatformDemo.Controllers
{
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
        public IActionResult Get()
        {
            return Ok(_db.Tickets.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _db.Tickets.Find(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            _db.SaveChanges();

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = ticket.TicketId },
                value: ticket);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            _db.Entry(ticket).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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
        public IActionResult Delete(int id)
        {
            var ticket = _db.Tickets.Find(id);
            if (ticket == null) return NotFound();

            _db.Tickets.Remove(ticket);
            _db.SaveChanges();

            return Ok(ticket);
        }
    }
}
