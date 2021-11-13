using Microsoft.AspNetCore.Mvc;
using PlatformDemo.Filters;
using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Version1DiscontinuedResourceFilter]
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
        public IActionResult PostV1([FromBody] Ticket ticket)
        {
            return Ok(ticket);
        }

        [HttpPost]
        [Route("/api/v2/tickets")] 
        [Ticket_EnsureEnteredDate]
        public IActionResult PostV2([FromBody] Ticket ticket)
        {
            // using filter we can implement new validation in the model
            // if we had used DataAnnotations, we could have broken the V1 
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
