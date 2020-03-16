using Microsoft.AspNetCore.Mvc;

namespace HideRoutesFromSwagger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = true)] // Method 1: Add this attribute on controller/action
    public class FirstController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { Something = "A" });
        }
    }
}
