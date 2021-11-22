using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.QueryFilters;

namespace PlatformDemo.Controllers
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    public class ProjectsV2Controller : ControllerBase
    {
        private readonly BugTrackerContext _db;

        public ProjectsV2Controller(BugTrackerContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _db.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }


        [HttpGet]
        [Route("/api/projects/{pId:int}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pId, [FromQuery] ProjectTicketQueryFilter filter)
        {
            IQueryable<Ticket> tickets = _db.Tickets.Where(t => t.ProjectId == pId);

            if (filter != null && !string.IsNullOrWhiteSpace(filter.Owner))
                tickets = tickets.Where(t => (t.Owner ?? "").Contains(filter.Owner, StringComparison.OrdinalIgnoreCase));

            var listTickets = await tickets.ToListAsync();
            if (listTickets == null || listTickets.Count <= 0)
                return NotFound();

            return Ok(listTickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {
            _db.Projects.Add(project);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = project.ProjectId },
                value: project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            _db.Entry(project).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch
            {
                if (await _db.Projects.FindAsync(id) == null)
                    return NotFound();

                throw; // generate a Internal Server Error 500
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _db.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _db.Projects.Remove(project);
            await _db.SaveChangesAsync();

            return Ok(project);
        }
    }
}
