using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            return Ok("Reading all the projects");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok($"Reading project #{id}");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok("Creating  a project");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Updating a project");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            return Ok("Deleting a project #{id}");
        }

        /// <summary>
        /// api/projects/{pid}/tickets?tid={tid}
        /// </summary>
        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public IActionResult GetProjectTicket(int pId, [FromQuery] int tId) // [FromQuery] optional
        {
            if (tId == 0)
                return Ok("Reading all tickets belong to the project {pId}");
            else

                return Ok($"Reading project #{pId}, ticket #{tId}");
        }
    }
}
