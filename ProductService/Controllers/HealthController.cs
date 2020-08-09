using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
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