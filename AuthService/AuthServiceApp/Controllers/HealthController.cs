using Microsoft.AspNetCore.Mvc;

namespace AuthServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        // GET
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
