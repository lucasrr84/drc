using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace collector.api.controller
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("GetStatus()\n");
        }
    }
}