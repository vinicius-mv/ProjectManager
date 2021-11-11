using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    public class TicketsController : ControllerBase
    {
        [HttpGet]
        [Route("api/tickets")]
        public IActionResult Get()
        {
            return Ok("Reading all the tickets");
        }

        [HttpGet]
        [Route("api/ticket/{id}")]
        public IActionResult Get(int id)
        {
            return Ok($"Reading ticket #{id}");
        }

        [HttpPost]
        [Route("api/tickets")]
        public IActionResult Post()
        {
            return Ok("Creating  a ticket");
        }

        [HttpPut]
        [Route("api/tickets")]
        public IActionResult Put()
        {
            return Ok("Updating a ticket");
        }

        [HttpDelete]
        [Route("api/ticket/{id}")]
        public IActionResult Delete()
        {
            return Ok("Deleting a ticket #{id}");
        }
    }
}
