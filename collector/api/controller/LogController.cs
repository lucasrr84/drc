using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace collector.api.controller
{
    [Route("[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetLog()
        {
            string mensagem = "GetLog()\n";
            return Ok(mensagem);
        }
    }
}
