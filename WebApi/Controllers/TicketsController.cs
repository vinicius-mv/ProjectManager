using Core.Models;
using Microsoft.AspNetCore.Mvc;
namespace PlatformDemo.Controllers
{
    [ApiController] 
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Reading all the tickets");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading ticket #{id}");
        }

        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a ticket");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            return Ok("Deleting a ticket #{id}");
        }
    }
}
