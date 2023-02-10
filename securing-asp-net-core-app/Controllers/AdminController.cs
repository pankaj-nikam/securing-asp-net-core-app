using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace securing_asp_net_core_app.Controllers
{
    //NOTE: 1C - Add roles on relevant controllers
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
