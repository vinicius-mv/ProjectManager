using Core.Models;
using DataStore.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly BugTrackerContext _db;

        public ProjectsController(BugTrackerContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_db.Projects.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _db.Projects.Find(id);
            if (project == null)
                return NotFound();

            return Ok(project);
        }


        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTickets(int pId)
        {
            var tickets = _db.Tickets.Where(t => t.ProjectId == pId).ToList();
            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Project project)
        {
            _db.Projects.Add(project);
            _db.SaveChanges();

            return CreatedAtAction(
                actionName: nameof(GetById),
                routeValues: new { id = project.ProjectId },
                value: project);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            _db.Entry(project).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch
            {
                if(_db.Projects.Find(id) == null)
                    return NotFound();

                throw; // generate a Internal Server Error 500
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var project = _db.Projects.Find(id);
            if(project == null) return NotFound();

            _db.Projects.Remove(project);
            _db.SaveChanges();

            return Ok(project);
        }
    }
}
