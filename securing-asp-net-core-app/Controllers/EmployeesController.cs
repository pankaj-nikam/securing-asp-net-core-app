using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace securing_asp_net_core_app.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
