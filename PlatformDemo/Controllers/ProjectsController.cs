using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controllers
{
    [ApiController]
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
    }
}
