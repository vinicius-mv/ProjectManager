using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Filters;
using WebApi.Filters.V2;
using WebApi.QueryFilters;

//TODO: filter using System.Web.Http.OData

namespace PlatformDemo.Controllers.V2
{
    [ApiKeyAuthFilter]
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketsV2Controller : ControllerBase
    {
        private readonly BugTrackerContext _db;

        public TicketsV2Controller(BugTrackerContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = _db.Tickets;

            if (ticketQueryFilter != null)
            {
                if (ticketQueryFilter.Id.HasValue)
                    tickets = tickets.Where(x => x.TicketId == ticketQueryFilter.Id);
                    
                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.TitleOrDescription))
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.TitleOrDescription, StringComparison.OrdinalIgnoreCase) || 
                        x.Description.Contains(ticketQueryFilter.TitleOrDescription, StringComparison.OrdinalIgnoreCase));

            }

            return Ok(await tickets.ToListAsync());
        }

        //[HttpGet]       // https://domain.com/api/tickets?$filter=id eq 1       // https://domain.com/api/tickets?$filter=id gt 1       
        //[EnableQuery]   // method 2 to enable data filtering by query - package System.Web.Http.OData; 
        //public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        //{
        //    IQueryable<Ticket> tickets = _db.Tickets;

        //    return Ok(await tickets.ToListAsync());
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _db.Tickets.FindAsync(id);
            if (ticket == null)
                return NotFound();

            return Ok(ticket);
        }

        [HttpPost]
        [Ticket_EnsureDescriptionPresentActionFilter]
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
        [Ticket_EnsureDescriptionPresentActionFilter]
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

            return Ok(ticket);
        }
    }
}
