using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace securing_asp_net_core_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DisableRateLimiting] //NOTE: 4C You can disable the rate limiting for certain controllers too.
    public class DemoApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetResults()
        {
            var items = new string[] { "Item 1", "Item 2", "Item 3" };
            return Ok(items);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")] //NOTE: 4B Add name of the policy for throttling the controller. 
    public class ThrottledDemoApiController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetResults()
        {
            await Task.Delay(1000);
            var items = new string[] { "Item 1", "Item 2", "Item 3" };
            return Ok(items);
        }
    }
}