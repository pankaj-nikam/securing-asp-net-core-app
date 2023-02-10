using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace securing_asp_net_core_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        //NOTE: 2A 
        private readonly IDataProtector _dataProtector;

        public SecurityController(IDataProtectionProvider provider)
        {
            _dataProtector = provider.CreateProtector("Encrypting.V1");
        }

        //NOTE: 2B
        [HttpPost("Encrypt")]
        //NOTE: 3
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Encrypt([FromForm]string data)
        {
            return Ok(_dataProtector.Protect(data));
        }

        //NOTE: 2C
        [HttpPost("Decrypt")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Decrypt([FromForm] string data)
        {
            return Ok(_dataProtector.Unprotect(data));
        }
    }
}